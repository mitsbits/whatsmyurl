using System;


namespace WhatsMyUrl.Dal.Model
{
    public interface IHubConnection<out TS, out TH >
    {

   
      
        TS SessionId { get; }
      
        TH HubId { get; }
        HubState HubState { get; set; }
        DateTime CreatedOn { get; }
    }
}