using System;


namespace FunctionApp1
{
    public class Constants
    {
        public const string BeginingEndingTotal = "BeginingEndingTotal";

        public const string ContentType = "application/xml";

        public const string req_Id = "BusUtId";
        public const string req_Name = "Name";
        public const string req_StartDate = "StartDate";
        public const string req_Batch = "Batch";

        public const string Msg_Common = "Something went wrong please try again";
        public const string Msg_InvalidBusUtId = "Invalid BusUtId (BusUtId should be greater than zero)";
        public const string Msg_InvalidBusDaydt = "Invalid BusDaydt (Date should be MM/dd/yyyy format)";
        public const string Msg_InvalidSrcCate = "Invalid Source Category (source category should be a single alphabet)";
    }
}
