namespace ChickenSoftware.StockAnalyzer
open FSharp.Data
open Accord
open Accord.Statistics
open Accord.Statistics.Models.Regression.Linear

type YahooContext = CsvProvider<"http://ichart.finance.yahoo.com/table.csv?s=MSFT">

type StockProvider() = 
    member this.GetMostRecentPrice ticker = 
        try
            let stockInfo = YahooContext.Load("http://ichart.finance.yahoo.com/table.csv?s=" + ticker).Rows
            stockInfo 
            |> Seq.map(fun si -> si.``Adj Close``)
            |> Seq.head
            |> float
        with | :? System.Net.WebException -> -1.0

    member this.PredictStockPrice ticker (targetDate:System.DateTime) =
        try
            let stockInfo = YahooContext.Load("http://ichart.finance.yahoo.com/table.csv?s=" + ticker).Rows
            let rows = stockInfo |> Seq.take(20)
            let x = rows |> Seq.map(fun si -> si.Date.ToOADate()) |> Seq.toArray
            let y = rows |> Seq.map(fun si -> (float)si.``Adj Close``) |> Seq.toArray

            let regression = SimpleLinearRegression()
            let sse = regression.Regress(x,y)
            let mse = sse/float x.Length 
            let rmse = sqrt(mse)
            let r2 = regression.CoefficientOfDetermination(x,y)

            let tomorrow = [|targetDate.ToOADate()|]
            regression.Compute(tomorrow) |> Seq.head
        with | :? System.Net.WebException -> -1.0
