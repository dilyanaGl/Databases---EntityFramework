using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Office.App.Contracts;
using Office.App.Dto;
using Office.Services;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class CreateEventCommand : ICommand
    {
        private const string SuccessMessage = "Event {0} was created successfully!";
        private const string InvalidDate = "Please insert the dates in format: [dd/MM/yyyy HH:mm]!";
        private const string StartDateBeforeEndDate = "Start date should be before end date.";
        private const string NoUser = "You should login first!";
        private const string InvalidDetails = "Invalid details!";
        private const string InvalidLength = "Invalid arguments count!";


        private readonly IEventService eventService;

        public CreateEventCommand(IEventService eventService)
        {
            this.eventService = eventService;

        }

        public string Execute(string[] data)
        {

            if (!Validation.CheckLength(4, data))
            {
                return InvalidLength;
            }

            if (Session.User == null)
            {
                return NoUser;

            }

           var dto = new CreateEventDto()
           {
               Name = data[0],
               Description = data[1],
               StartDate = data[2],
               EndDate = data[3]
               
           };

            if (!Validation.IsValid(dto))
            {
                return InvalidDetails;
            }

            if (!Validation.ValidateDate(dto.StartDate) || 
                !Validation.ValidateDate(dto.EndDate))
            {
                return InvalidDate;
            }

            DateTime startDate = DateTime.ParseExact(dto.StartDate, "dd/MM/yyyyHH:mm", CultureInfo.InvariantCulture); ;
            DateTime endDate = DateTime.ParseExact(dto.EndDate, "dd/MM/yyyyHH:mm", CultureInfo.InvariantCulture);

            if (startDate > endDate)
            {

                return StartDateBeforeEndDate;
            }
            
            eventService.CreateEvent(dto.Name, dto.Description, startDate, endDate, Session.User.Username);

            return String.Format(SuccessMessage, dto.Name);

        }
    }
}
