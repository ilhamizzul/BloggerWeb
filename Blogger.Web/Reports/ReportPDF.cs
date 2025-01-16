using Blogger.Web.Models.Domain;
using Blogger.Web.Repositories;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Blogger.Web.Reports
{
    public class ReportPDF : IDocument
    {
        private readonly IEnumerable<BlogPost> dataPosts;
        private string startAt;
        private string endAt;

        public ReportPDF(IEnumerable<BlogPost> dataPosts, string date)
        {
            this.dataPosts = dataPosts;
            this.startAt = Convert.ToDateTime(date).AddDays(-30).ToString("yyyy-MM-dd");
            this.endAt = Convert.ToDateTime(date).ToString("yyyy-MM-dd");

        }
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        
        public async void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A3.Landscape());
                page.Margin(1, Unit.Centimetre);
                page.DefaultTextStyle(TextStyle.Default.Size(12));
                page.PageColor(Colors.White);

                page.Header().Element(ComposeHeader);


                // First Content: The Legend
                /*page.Content().AlignCenter().Column(column =>
                {
                    column.Item().Text("Legenda Durasi").FontSize(14).Bold().AlignCenter();

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().AlignRight().Text(x => x.Span("● ").FontColor(Colors.Green.Medium));
                        row.RelativeItem(3).AlignLeft().Text("durasi <= SLA");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().AlignRight().Text(x => x.Span("● ").FontColor(Colors.Orange.Medium));
                        row.RelativeItem(3).AlignLeft().Text("SLA > durasi <= 1.5x SLA");
                    });

                    column.Item().Row(row =>
                    {
                        row.RelativeItem().AlignRight().Text(x => x.Span("● ").FontColor(Colors.Red.Medium));
                        row.RelativeItem(3).AlignLeft().Text("durasi > 1.5x SLA");
                    });
                    column.Spacing(10); // Add spacing between legend and table
                });*/

                //page.Content().PaddingTop(10).Column(column =>
                //{
                //    column.Spacing(10);
                //    column.Item().AlignCenter().Column(column =>
                //    {
                //        column.Spacing(5);
                //        column.Item().Text("Legenda Durasi").FontSize(10).Bold().AlignCenter();
                //        column.Item().Row(row =>
                //        {
                //            row.RelativeItem().AlignRight().Text(x => x.Span("● ").FontColor(Colors.Green.Medium));
                //            row.RelativeItem(3).AlignLeft().Text("durasi <= SLA").FontSize(8);
                //            row.RelativeItem().AlignRight().Text(x => x.Span("● ").FontColor(Colors.Orange.Medium));
                //            row.RelativeItem(3).AlignLeft().Text("SLA > durasi <= 1.5x SLA").FontSize(8);
                //            row.RelativeItem().AlignRight().Text(x => x.Span("● ").FontColor(Colors.Red.Medium));
                //            row.RelativeItem(3).AlignLeft().Text("durasi > 1.5x SLA").FontSize(8);
                //        });

                //        //column.Item().Row(row =>
                //        //{
                //        //    row.RelativeItem().AlignRight().Text(x => x.Span("● ").FontColor(Colors.Orange.Medium));
                //        //    row.RelativeItem(3).AlignLeft().Text("SLA > durasi <= 1.5x SLA");
                //        //});

                //        //column.Item().Row(row =>
                //        //{
                //        //    row.RelativeItem().AlignRight().Text(x => x.Span("● ").FontColor(Colors.Red.Medium));
                //        //    row.RelativeItem(3).AlignLeft().Text("durasi > 1.5x SLA");
                //        //});
                //    });

                //    column.Item().Table(table =>
                //    {

                //        table.ColumnsDefinition(columns =>
                //        {
                //            columns.RelativeColumn();
                //            columns.RelativeColumn();
                //            columns.RelativeColumn();
                //            columns.RelativeColumn();
                //        });

                //        table.Header(header =>
                //        {
                //            header.Cell().Element(CellStyle).Text("Blog Title").FontSize(14).Bold();
                //            header.Cell().Element(CellStyle).Text("Heading").FontSize(14).Bold();
                //            header.Cell().Element(CellStyle).Text("Author").FontSize(14).Bold();
                //            header.Cell().Element(CellStyle).Text("Published Date").FontSize(14).Bold();

                //            static IContainer CellStyle(IContainer container) => container
                //            .Border(1)
                //            .Background(Colors.Grey.Lighten3)
                //            .Padding(5)
                //            .AlignCenter()
                //            .AlignMiddle();
                //        });

                //        foreach (var item in dataPosts)
                //        {
                //            table.Cell().Element(CellStyle).Text(item.PageTitle);
                //            table.Cell().Element(CellStyle).Text(item.Heading);
                //            table.Cell().Element(CellStyle).Text(item.Author);
                //            table.Cell().Element(CellStyle).Text(item.PublishedDate);
                //        }

                //        static IContainer CellStyle(IContainer container) => container
                //        .Border(1)
                //        .Padding(5)
                //        .AlignLeft()
                //        .AlignMiddle();

                //    });

                //});

                //Second Content: The Table
                page.Content().PaddingTop(10).Table(table =>
                {

                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Id").FontSize(14).Bold();
                        header.Cell().Element(CellStyle).Text("Blog Title").FontSize(14).Bold();
                        header.Cell().Element(CellStyle).Text("Heading").FontSize(14).Bold();
                        header.Cell().Element(CellStyle).Text("Author").FontSize(14).Bold();
                        header.Cell().Element(CellStyle).Text("Published Date").FontSize(14).Bold();
                        header.Cell().Element(CellStyle).Text("Short Desc").FontSize(14).Bold();

                        static IContainer CellStyle(IContainer container) => container
                        .Border(1)
                        .Background(Colors.Grey.Lighten3)
                        .Padding(5)
                        .AlignCenter()
                        .AlignMiddle();
                    });

                    foreach (var item in dataPosts)
                    {
                        table.Cell().Element(CellStyle).Text(item.Id);
                        table.Cell().Element(CellStyle).Text(item.PageTitle);
                        table.Cell().Element(CellStyle).Text(item.Heading);
                        table.Cell().Element(CellStyle).Text(item.Author);
                        table.Cell().Element(CellStyle).Text(item.PublishedDate);
                        table.Cell().Element(CellStyle).Text(item.ShortDescription);
                    }

                    static IContainer CellStyle(IContainer container) => container
                    .Border(1)
                    .Padding(5)
                    .AlignLeft()
                    .AlignMiddle();

                });

                page.Footer().Row(row =>
                {

                    row.RelativeItem().AlignRight().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" of ");
                        x.TotalPages();
                    });
                });
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem(5).ShowOnce().AlignLeft().AlignMiddle().Column(column =>
                {
                    column.Item().Text("Report Truck Over SLA").FontSize(16).Bold();
                    column.Item().Text("Periode "+startAt+" s/d "+endAt+" (30 Hari)").FontSize(12).Light();
                }); // Optional title
                row.RelativeItem(0.8f).ShowOnce().AlignRight().AlignMiddle().PaddingRight(10).Element(c => ComposeLogo(c, "pupuk_indonesia-removebg.png")); // Place the image
                row.RelativeItem(1).ShowOnce().AlignRight().AlignMiddle().Element(c => ComposeLogo(c, "dpcs-removebg.png")); // Place the image
            });
        }

        private void ComposeLogo(IContainer container, string fileName)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image", "logo", fileName);

            if (File.Exists(imagePath))
            {
                var imageBytes = File.ReadAllBytes(imagePath);
                container.Scale(0.001f).Image(imageBytes).FitArea();
                    
            } else
            {
                container.Text(fileName).FontSize(12);
            }

        }
    }
}
