using Amazon.Translate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace erecord_translate
{
    class Program
    {

        static string DataBasePath;
        static string UserId;
        static string UserPass;

        static string itmn;
        static string lngv;
        static string genv;
        static string taiv;

        const string Config = "setting.xml";
        const string TerminologyName = "ykk_jisho";

        static void Main(string[] args)
        {
            // 実行時間を計る・開始時間をログに入れる
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            WriteLog.LogFileWriteProc("awstranslate", "Program Start", "");

            // DB機能用の処理追加
            DataSet ds = new DataSet();
            DataTable dt;

            //クエリパラメーター用
            CommandParameters cmdParams = new CommandParameters();

            //コンフィグファイルの読込
            ds.ReadXml(Config);

            //データベース関連
            dt = ds.Tables["database"];
            DataBasePath = dt.Rows[0]["path"].ToString();
            UserId = dt.Rows[0]["user"].ToString();
            UserPass = dt.Rows[0]["pass"].ToString();

            using (DatabaseAccess dta = new DatabaseAccess(UserId, UserPass, DataBasePath))
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.AppendLine("SELECT ITMN,LNGV,GENV,TAIV"); //項目指定
                sb1.AppendLine("FROM MST_MNTN_NOTE "); //原因対策マスタ
                sb1.AppendLine("WHERE HAND_TRNS_FLGV = 1 AND HAND_UPDV = 1 "); // 条件
                sb1.AppendLine("ORDER BY ITMN ASC"); //並び替え

                string query1 = sb1.ToString();

                dt = dta.SelectQuery(query1);
            }

            //var awsOptions = TranslateService.BuildAwsOptions();
            //var service = new TranslateService(awsOptions.CreateServiceClient<IAmazonTranslate>());

            //AmazonTranslateのサービス設定
            //インターネット経由：プロキシ設定
            //ProxyHost = "172.23.110.249"
            //directconnect経由：VPCエンドポイントのプライベートDNS
            var config = new AmazonTranslateConfig
            {
                ServiceURL = "https://translate.ap-northeast-1.amazonaws.com"
            };
            var client = new AmazonTranslateClient(config);
            var service = new TranslateService(client);

            // カスタム用語集で翻訳
            // 用語集CSVファイルを読み込む
            var ms = new MemoryStream();
            var fs1 = new FileStream("ykk_jisho.csv", FileMode.Open);
            fs1.CopyTo(ms);

            // 用語集を設定する
            service.SetTerminology(TerminologyName, ms).Wait();

            // 用語集を用いてクエリを出す
            var terminologies = new List<string>() { TerminologyName };

            var fs2 = new FileStream("lang_list.csv", FileMode.Open);
            using (StreamReader sr = new StreamReader(fs2))
            {
                List<string> er_lang = new List<string>();
                List<string> aws_lang = new List<string>();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var values = line.Split(',');

                    er_lang.Add(values[0]);
                    aws_lang.Add(values[1]);
                }

                int cnt = 0;
                int sqlcnt = 0;
                int totalsqlcnt = 0;

                foreach (DataRow dr in dt.Rows)
                {

                    itmn = dr[0].ToString().Trim();
                    lngv = dr[1].ToString().Trim();
                    genv = dr[2].ToString().Trim();
                    taiv = dr[3].ToString().Trim();

                    for (int i = 1; i < aws_lang.Count; i++)
                    {
                        if (er_lang[i].Contains(lngv))
                        {
                            var aws_lang1 = aws_lang[i];

                            for (int j = 1; j < aws_lang.Count; j++)
                            {
                                if (j != i)
                                {
                                
                                    var aws_lang2 = aws_lang[j];

                                    //原因の翻訳
                                    var translatetask_genv = service.TranslateText(genv, aws_lang1, aws_lang2, terminologies);
                                    var result_genv = translatetask_genv.Result;
                                    var trns_genv = result_genv.TranslatedText;

                                    //対策の翻訳
                                    var translatetask_taiv = service.TranslateText(taiv, aws_lang1, aws_lang2, terminologies);
                                    var result_taiv = translatetask_taiv.Result;
                                    var trns_taiv = result_taiv.TranslatedText;

                                    trns_genv = trns_genv.Replace("'", "");
                                    trns_taiv = trns_taiv.Replace("'", "");

                                    using (DatabaseAccess dta = new DatabaseAccess(UserId, UserPass, DataBasePath))
                                    {
                                        //StringBuilder sb2 = new StringBuilder();
                                        //sb2.AppendLine("INSERT INTO MST_MNTN_NOTE "); //挿入文と指定テーブル
                                        //sb2.AppendLine("(ITMN, LNGV, GENV, TAIV, HAND_TRNS_FLGV, HAND_UPDV, TRNS_UPDD) "); //項目指定
                                        //sb2.AppendLine("VALUES ('" + itmn + "','" + er_lang[j] + "','" + trns_genv + "','" + trns_taiv + "','2','2',sysdate)"); // 値指定

                                        StringBuilder sb2 = new StringBuilder();
                                        sb2.AppendLine("MERGE INTO MST_MNTN_NOTE x"); //挿入文と指定テーブル
                                        sb2.AppendLine("USING (SELECT 1 FROM dual) "); //項目指定
                                        sb2.AppendLine("ON (x.ITMN  = '" + itmn + "' AND x.LNGV = '" + er_lang[j] + "')"); //項目指定
                                        sb2.AppendLine("WHEN MATCHED THEN"); //項目指定
                                        sb2.AppendLine("UPDATE SET x.GENV = '" + trns_genv + "',x.TAIV ='" + trns_taiv + "',x.HAND_TRNS_FLGV = '2',x.HAND_UPDV = '2',x.TRNS_UPDD = sysdate"); // 値指定
                                        sb2.AppendLine("WHERE (x.ITMN  = '" + itmn + "' AND x.LNGV = '" + er_lang[j] + "')"); //項目指定
                                        sb2.AppendLine("WHEN NOT MATCHED THEN"); //項目指定
                                        sb2.AppendLine("INSERT(ITMN, LNGV, GENV, TAIV, HAND_TRNS_FLGV, HAND_UPDV, TRNS_UPDD) "); //項目指定
                                        sb2.AppendLine("VALUES ('" + itmn + "','" + er_lang[j] + "','" + trns_genv + "','" + trns_taiv + "','2','2',sysdate)"); // 値指定

                                        string query2 = sb2.ToString();

                                        sqlcnt = dta.UpdateQuery(query2, cmdParams);
                                        totalsqlcnt += sqlcnt;

                                        //WriteLog.LogFileWriteProc(itmn, er_lang[j], trns_genv);
                                        //WriteLog.LogFileWriteProc(itmn, er_lang[j], trns_taiv);
                                    }
                                }
                            }

                            using (DatabaseAccess dta = new DatabaseAccess(UserId, UserPass, DataBasePath))
                            {

                                StringBuilder sb3 = new StringBuilder();
                                sb3.AppendLine("UPDATE MST_MNTN_NOTE "); //挿入文と指定テーブル
                                sb3.AppendLine("SET HAND_UPDV = '2' "); //項目指定
                                sb3.AppendLine("WHERE ITMN ='" + itmn + "' AND LNGV = '" + lngv + "'"); // 値指定

                                string query3 = sb3.ToString();

                                dta.UpdateQuery(query3, cmdParams);
                            }
                        }
                    }

                    cnt += 1;
                    if (cnt > 1)
                    {

                        // 翻訳件数・実行時間・終了時間をログに入れる
                        WriteLog.LogFileWriteProc("awstranslate", "Translate Count", totalsqlcnt.ToString());
                        stopWatch.Stop();
                        TimeSpan ts = stopWatch.Elapsed;
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                        WriteLog.LogFileWriteProc("awstranslate", "Program Runtime", elapsedTime);
                        WriteLog.LogFileWriteProc("awstranslate", "Program End", "\n");

                        break;
                    }
                }
            }
            fs1.Close();
            fs1.Dispose();
            fs2.Close();
            fs2.Dispose();
        }
    }
}
