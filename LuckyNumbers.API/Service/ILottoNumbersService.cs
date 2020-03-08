using LuckyNumbers.API.Dtos;
using LuckyNumbers.API.Entities;

namespace LuckyNumbers.API.Service
{
    public interface ILottoNumbersService
    {
         bool isUserHaveSaldo(int amountBets, int saldo);
         bool isUserHaveSaldo(int saldo);
         int[] tabOfLottoNumbersDto(LottoNumbersDto lottoNumbersDto);
         UserLottoBets inputNumbers(LottoNumbersDto lottoNumbersDto, int userId);
         void mapNumbersToUserLottoBets(ref UserLottoBets userLottoBets, int[] numbers);
         int[] generateNumbers();
         bool isNumberDuplicated(int[] lottoNumbersDto);
         bool isCorrectRange(int[] lottoNumbers);
         void sortLottoNumbers( int[] lottoNumbers );
    }
}