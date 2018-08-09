using System;
using System.Collections.Generic;
using System.Text;
using Office.App.Contracts;
using Office.Services;
using Office.Services.Contracts;

namespace Office.App.Commands
{
    public class ShowEventCommand : ICommand
    {
        private const string EventNotFound = "Event {0} not found!";
        private const string InvalidLength = "Invalid arguments count!";


        private readonly IEventService eventService;
       
        public ShowEventCommand(IEventService eventService)
        {
            this.eventService = eventService;
          
        }

        public string Execute(string[] data)
        {
            if (!Validation.CheckLength(1, data))
            {
                return InvalidLength;
            }

            string eventName = data[0];
            if (!eventService.Exists(eventName))
            {
                return String.Format(EventNotFound, eventName);
            }

            return eventService.ShowEvent(eventName);
        }
    }
}
