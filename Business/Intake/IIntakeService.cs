using NutriCore.Models;

namespace NutriCore.Business;

public interface IIntakeService
{
    Intake RegisterIntake(IntakeCreateUpdateDto dto);
    IEnumerable<Intake> GetIntakes();
    IEnumerable<Intake> GetIntakesByUser(int userId);
    Intake GetIntakeById(int intakeId);
    void UpdateIntake(int intakeId, IntakeCreateUpdateDto dto);
    void DeleteIntake(int intakeId);
}