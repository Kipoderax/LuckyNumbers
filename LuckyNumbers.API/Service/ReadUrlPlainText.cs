using System;
using System.Net;

namespace LuckyNumbers.API.Service
{
    public class ReadUrlPlainText
    {

        public string readlatestDataLottoGame() {

            string latestDataLottoGame = readUrlData("https://app.lotto.pl/wyniki/?type=dl").Substring(0, 10);

            return latestDataLottoGame;
        
        }
        public int[] readRawLatestLottoNumbers(){
            int[] lottos = new int[6];
            string latestLottoGameNumbers = readUrlData("https://app.lotto.pl/wyniki/?type=dl");
            
            string[] getLottoNumbers = latestLottoGameNumbers.Split(Environment.NewLine.ToCharArray());
            
            for (int i = 0; i < lottos.Length; i++) {
                lottos[i] = int.Parse(getLottoNumbers[i+1]);
            }

            return lottos;
        }

        public string readPriceForGoalLottoNumbers() {

            return "";
        }

        private string readUrlData(string url) {
            WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData(url);

            string webData = System.Text.Encoding.UTF8.GetString(raw);

            return webData;
        }
    }
}