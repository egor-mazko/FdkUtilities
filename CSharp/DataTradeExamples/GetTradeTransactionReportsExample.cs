﻿namespace DataTradeExamples
{
    using System;
    using System.Threading;
    using SoftFX.Extended;
    using SoftFX.Extended.Events;

    class GetTradeTransactionReportsExample : Example
    {
        public GetTradeTransactionReportsExample(string address, string username, string password)
            : base(address, username, password)
        {
            this.Trade.TradeTransactionReport += this.OnTradeTransactionReport;
        }

        protected override void RunExample()
        {
            var to = DateTime.UtcNow;
            var from = to.AddDays(-1);

            var reportsNumber = 0;
            var it = this.Trade.Server.GetTradeTransactionReports(TimeDirection.Forward, true, null, null, 100);
            for (; !it.EndOfStream; it.Next())
            {
                reportsNumber++;
            }
            it.Dispose();
            Console.WriteLine("Reports number = {0}", reportsNumber);
            var position = this.Trade.Server.SendOrder("EURUSD", TradeCommand.Market, TradeRecordSide.Buy, 0, 1000000, null, null, null, null);
            var result = position.Close();
            Thread.Sleep(1000);
        }

        void OnTradeTransactionReport(object sender, TradeTransactionReportEventArgs e)
        {
            Console.WriteLine("New trade transaction report has been received");
        }
    }
}