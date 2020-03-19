using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LuckyNumbers.API.Data;
using LuckyNumbers.API.Entities;
using Quartz;

namespace LuckyNumbers.API.Service
{
    public class CheckDate : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            System.Console.WriteLine("test");
             
            // changeAvailableShowResult();
            try {
                Debug.WriteLine("Is running");
            } catch(Exception ex) {
                System.Console.WriteLine(ex);
            }
            
            return Task.FromResult(0);
        }

        public void changeAvailableShowResult() {
            // LottoGame lottoGame = new LottoGame();
            // int lastUserId = userRepo.getLastId();

            // for (int i = 1; i <= lastUserId; i++) {

            // }
            // lottoGame.userId = 8;
            // lottoGame.resultCheck = 1;

            // userRepo.add(lottoGame);

            // userRepo.saveAll();
        }
    }
}