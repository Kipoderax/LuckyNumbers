using LuckyNumbers.API.Dtos;

namespace LuckyNumbers.API.Service
{
    public interface ILottoNumbersService
    {
         void sendGenerateNumbers(int userId, int amountBetsToSend);
         void sendInputNumbers(int userId, int[] lottoNumbersDto);
         bool isUserHaveSaldo(int saldo);
         int[] tabOfLottoNumbersDto(LottoNumbersDto lottoNumbersDto);
         bool isNumberDuplicated(int[] tabOfLottoNumbers);
         bool isCorrectRange(LottoNumbersDto lottoNumbersDto);
         void sortLottoNumbers( int[] lottoNumbers );
    }
}