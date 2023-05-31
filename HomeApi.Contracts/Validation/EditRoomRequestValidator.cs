using FluentValidation;
using HomeApi.Contracts.Models.Rooms;
using System.Linq;

namespace HomeApi.Contracts.Validation
{
    public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
    {
        public EditRoomRequestValidator()
        {
            RuleFor(x => x.NewName)
                .NotEmpty()
                .Must(BeSupportedRooms)
                .WithMessage($"Поддерживаются следующие коинаты {string.Join(", ", Values.ValidRooms)}");
            RuleFor(x => x.NewArea).NotEmpty();
            RuleFor(x => x.NewGasConnected).NotEmpty();
            RuleFor(x => x.NewVoltage)
                .NotEmpty()
                .Must(BeSupportedVoltage)
                .WithMessage($"Напряжение должно быть 110/220 вольт");
        }

        private bool BeSupportedRooms(string rooms)
        {
            return Values.ValidRooms.Any(x => x == rooms);
        }

        private bool BeSupportedVoltage(int voltage)
        {
            if (voltage < 110)
                return false;

            if (voltage > 220)
                return false;

            return true;
        }
    }
}
