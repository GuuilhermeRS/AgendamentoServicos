using AgendamentoServicos.Core.Dtos;
using AgendamentoServicos.Core.Model;

namespace AgendamentoServicos.Core.Interfaces.Services;

public interface ISlotService
{
    Task<IEnumerable<SlotDto>> GetAll();
    Task<SlotDto?> GetById(int id);
    Task<Slot> Create(CreateSlotDto dto);
    Task<Slot> Cancel(int id);
    Task<Slot> Complete(int id);
}