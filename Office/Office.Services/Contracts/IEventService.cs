using System;
using Office.Models;

namespace Office.Services.Contracts
{
    public interface IEventService
    {
        Event ByName(string name);
        void CreateEvent(string name, string description, DateTime startDate, DateTime endDate, string creatorName);
        bool Exists(string name);
        string ShowEvent(string eventName);
    }
}