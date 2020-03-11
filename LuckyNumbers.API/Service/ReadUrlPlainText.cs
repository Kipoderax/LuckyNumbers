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

        public int[] readPriceForGoalLottoNumbers() {
            string getUrl = readUrlData("https://app.lotto.pl/wygrane/?type=dl");
            int[] rewardsMoney = new int[4];
            string[] getData = getUrl.Split(Environment.NewLine.ToCharArray());

            int.TryParse(getData[4].Substring(6, 2), out rewardsMoney[0]); // za trojke
            int.TryParse(getData[3].Substring(5, 3), out rewardsMoney[1]); // za czworke
            int.TryParse(getData[2].Substring(3, 4), out rewardsMoney[2]); // za piatke
            int.TryParse(getData[1].Substring(0), out rewardsMoney[3]); // za szostke
            if(rewardsMoney[3] == 0)
                rewardsMoney[3] = 2_000_000;

            return rewardsMoney;
        }

        private string readUrlData(string url) {
            WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData(url);

            string webData = System.Text.Encoding.UTF8.GetString(raw);

            return webData;
        }
    }
}