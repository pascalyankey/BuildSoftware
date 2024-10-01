using Client.Models;
using Domain.Responses;
using MediatR;

namespace Client.Interfaces
{
    public interface IClientService
    {
        Task<List<WerfViewModel>> GetWervenAsync();
        Task<List<WerknemerViewModel>> GetWerknemersByWerfNaam(string werfNaam);
        Task<Dictionary<int, WerfWerknemerInsertedResponse>> WerknemerToevoegen(WerknemerViewModel werknemer);
        Task<Unit> DeleteWerknemer(int id);
        Task<Dictionary<int, WerfWerknemerInsertedResponse>> WerfToevoegen(WerfViewModel werf);
    }
}
