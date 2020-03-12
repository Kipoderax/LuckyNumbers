using LuckyNumbers.API.Dtos;

namespace LuckyNumbers.API.Service
{
    public interface IResultUserLottoNumbers
    {
         ResultLottoDto resultLottoGame(int userId);
    }
}