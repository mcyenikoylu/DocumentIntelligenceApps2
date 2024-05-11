// using Azure;
// using Azure.AI.FormRecognizer.DocumentAnalysis;
// // using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
// // using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

// // //set `<your-endpoint>` and `<your-key>` variables with the values from the Azure portal to create your `AzureKeyCredential` and `DocumentAnalysisClient` instance
// // string endpoint = "https://eastus.api.cognitive.microsoft.com/";
// // string key = "4f4a8982dbe844e6888e8cc404665e20";

// string endpoint = "https://receiptgasinstance.cognitiveservices.azure.com/";
// string key = "611fa480b65e4c7aba92380e47389838";

// AzureKeyCredential credential = new AzureKeyCredential(key);
// DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

// //sample document
// //Uri fileUri = new Uri("https://www.spailor.com/wp-content/uploads/azureai/IMG_3754.jpeg");
// Uri fileUri = new Uri("https://www.spailor.com/wp-content/uploads/azureai/IMG_3758.jpeg");
// AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-receipt", fileUri);

// AnalyzeResult result = operation.Value;

// foreach (DocumentPage page in result.Pages)
// {
//     Console.WriteLine($"Document Page {page.PageNumber} has {page.Lines.Count} line(s), {page.Words.Count} word(s),");
//     Console.WriteLine($"and {page.SelectionMarks.Count} selection mark(s).");

//     for (int i = 0; i < page.Lines.Count; i++)
//     {
//         DocumentLine line = page.Lines[i];
//         Console.WriteLine($"  Line {i} has content: '{line.Content}'.");

//         Console.WriteLine($"    Its bounding box is:");
//         Console.WriteLine($"      Upper left => X: {line.BoundingPolygon[0].X}, Y= {line.BoundingPolygon[0].Y}");
//         Console.WriteLine($"      Upper right => X: {line.BoundingPolygon[1].X}, Y= {line.BoundingPolygon[1].Y}");
//         Console.WriteLine($"      Lower right => X: {line.BoundingPolygon[2].X}, Y= {line.BoundingPolygon[2].Y}");
//         Console.WriteLine($"      Lower left => X: {line.BoundingPolygon[3].X}, Y= {line.BoundingPolygon[3].Y}");
//     }

//     for (int i = 0; i < page.SelectionMarks.Count; i++)
//     {
//         DocumentSelectionMark selectionMark = page.SelectionMarks[i];

//         Console.WriteLine($"  Selection Mark {i} is {selectionMark.State}.");
//         Console.WriteLine($"    Its bounding box is:");
//         Console.WriteLine($"      Upper left => X: {selectionMark.BoundingPolygon[0].X}, Y= {selectionMark.BoundingPolygon[0].Y}");
//         Console.WriteLine($"      Upper right => X: {selectionMark.BoundingPolygon[1].X}, Y= {selectionMark.BoundingPolygon[1].Y}");
//         Console.WriteLine($"      Lower right => X: {selectionMark.BoundingPolygon[2].X}, Y= {selectionMark.BoundingPolygon[2].Y}");
//         Console.WriteLine($"      Lower left => X: {selectionMark.BoundingPolygon[3].X}, Y= {selectionMark.BoundingPolygon[3].Y}");
//     }
// }

// foreach (DocumentStyle style in result.Styles)
// {
//     // Check the style and style confidence to see if text is handwritten.
//     // Note that value '0.8' is used as an example.

//     bool isHandwritten = style.IsHandwritten.HasValue && style.IsHandwritten == true;

//     if (isHandwritten && style.Confidence > 0.8)
//     {
//         Console.WriteLine($"Handwritten content found:");

//         foreach (DocumentSpan span in style.Spans)
//         {
//             Console.WriteLine($"  Content: {result.Content.Substring(span.Index, span.Length)}");
//         }
//     }
// }

// Console.WriteLine("The following tables were extracted:");

// for (int i = 0; i < result.Tables.Count; i++)
// {
//     DocumentTable table = result.Tables[i];
//     Console.WriteLine($"  Table {i} has {table.RowCount} rows and {table.ColumnCount} columns.");

//     foreach (DocumentTableCell cell in table.Cells)
//     {
//         Console.WriteLine($"    Cell ({cell.RowIndex}, {cell.ColumnIndex}) has kind '{cell.Kind}' and content: '{cell.Content}'.");
//     }
// }

// ///////////

// // for (int i = 0; i < result.Documents.Count; i++)
// // {
// //     Console.WriteLine($"Document {i.ToString()}:");

// //     AnalyzedDocument document = result.Documents[i];

// //     if (document.Fields.TryGetValue("VendorName", out DocumentField vendorNameField))
// //     {
// //         if (vendorNameField.FieldType == DocumentFieldType.String)
// //         {
// //             string vendorName = vendorNameField.Value.AsString();
// //             Console.WriteLine($"Vendor Name: '{vendorName}', with confidence {vendorNameField.Confidence}");
// //         }
// //     }

// //     if (document.Fields.TryGetValue("CustomerName", out DocumentField customerNameField))
// //     {
// //         if (customerNameField.FieldType == DocumentFieldType.String)
// //         {
// //             string customerName = customerNameField.Value.AsString();
// //             Console.WriteLine($"Customer Name: '{customerName}', with confidence {customerNameField.Confidence}");
// //         }
// //     }

// //     if (document.Fields.TryGetValue("Items", out DocumentField itemsField))
// //     {
// //         if (itemsField.FieldType == DocumentFieldType.List)
// //         {
// //             foreach (DocumentField itemField in itemsField.Value.AsList())
// //             {
// //                 Console.WriteLine("Item:" + itemField.Content.ToString());

// //                 if (itemField.FieldType == DocumentFieldType.Dictionary)
// //                 {
// //                     IReadOnlyDictionary<string, DocumentField> itemFields = itemField.Value.AsDictionary();

// //                     if (itemFields.TryGetValue("Description", out DocumentField itemDescriptionField))
// //                     {
// //                         if (itemDescriptionField.FieldType == DocumentFieldType.String)
// //                         {
// //                             string itemDescription = itemDescriptionField.Value.AsString();

// //                             Console.WriteLine($"  Description: '{itemDescription}', with confidence {itemDescriptionField.Confidence.ToString()}");
// //                         }
// //                     }

// //                     if (itemFields.TryGetValue("Amount", out DocumentField itemAmountField))
// //                     {
// //                         if (itemAmountField.FieldType == DocumentFieldType.Currency)
// //                         {
// //                             CurrencyValue itemAmount = itemAmountField.Value.AsCurrency();

// //                             Console.WriteLine($"  Amount: '{itemAmount.Symbol}{itemAmount.Amount}', with confidence {itemAmountField.Confidence}");
// //                         }
// //                     }


// //                     if (itemFields.TryGetValue("ProductCode", out DocumentField itemProductCodeField))
// //                     {
// //                         if (itemDescriptionField.FieldType == DocumentFieldType.String)
// //                         {
// //                             string itemProductCode = itemDescriptionField.Value.AsString();

// //                             Console.WriteLine($"  ProductCode: '{itemProductCode}', with confidence {itemDescriptionField.Confidence.ToString()}");
// //                         }
// //                     }


// //                 }
// //             }
// //         }
// //     }

// //     if (document.Fields.TryGetValue("SubTotal", out DocumentField subTotalField))
// //     {
// //         if (subTotalField.FieldType == DocumentFieldType.Currency)
// //         {
// //             CurrencyValue subTotal = subTotalField.Value.AsCurrency();
// //             Console.WriteLine($"Sub Total: '{subTotal.Symbol}{subTotal.Amount}', with confidence {subTotalField.Confidence}");
// //         }
// //     }

// //     if (document.Fields.TryGetValue("TotalTax", out DocumentField totalTaxField))
// //     {
// //         if (totalTaxField.FieldType == DocumentFieldType.Currency)
// //         {
// //             CurrencyValue totalTax = totalTaxField.Value.AsCurrency();
// //             Console.WriteLine($"Total Tax: '{totalTax.Symbol}{totalTax.Amount}', with confidence {totalTaxField.Confidence}");
// //         }
// //     }

// //     if (document.Fields.TryGetValue("InvoiceTotal", out DocumentField invoiceTotalField))
// //     {
// //         if (invoiceTotalField.FieldType == DocumentFieldType.Currency)
// //         {
// //             CurrencyValue invoiceTotal = invoiceTotalField.Value.AsCurrency();
// //             Console.WriteLine($"Invoice Total: '{invoiceTotal.Symbol}{invoiceTotal.Amount}', with confidence {invoiceTotalField.Confidence}");
// //         }
// //     }
// // }

// // Display the found text.
// Console.WriteLine();

// foreach (DocumentPage page in result.Pages)
// {
//     foreach (DocumentLine line in page.Lines)
//     {
//         if (line.Content.Contains("34"))
//             Console.WriteLine(line.Content);
//     }
// }

/*
  This code sample shows Custom Model operations with the Azure Form Recognizer client library. 

  To learn more, please visit the documentation - Quickstart: Document Intelligence (formerly Form Recognizer) SDKs
  https://learn.microsoft.com/azure/ai-services/document-intelligence/quickstarts/get-started-sdks-rest-api?pivots=programming-language-csharp
*/

using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

/*
  Remember to remove the key from your code when you're done, and never post it publicly. For production, use
  secure methods to store and access your credentials. For more information, see 
  https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-security?tabs=command-line%2Ccsharp#environment-variables-and-application-configuration
*/

string endpoint = "https://eastus.api.cognitive.microsoft.com/";
string apiKey = "4f4a8982dbe844e6888e8cc404665e20";
AzureKeyCredential credential = new AzureKeyCredential(apiKey);
DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

string modelId = "receipt-gas-tr-model-mcy";
Uri fileUri = new Uri("https://www.spailor.com/wp-content/uploads/azureai/IMG_4436.jpg");

AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, modelId, fileUri);
AnalyzeResult result = operation.Value;

Console.WriteLine($"Document was analyzed with model with ID: {result.ModelId}");

foreach (AnalyzedDocument document in result.Documents)
{
    Console.WriteLine($"Document of type: {document.DocumentType}");

    foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
    {
        string fieldName = fieldKvp.Key;
        DocumentField field = fieldKvp.Value;

        Console.WriteLine($"Field '{fieldName}': ");

        Console.WriteLine($"  Content: '{field.Content}'");
        Console.WriteLine($"  Confidence: '{field.Confidence}'");
    }
}

// Iterate over lines and selection marks on each page
foreach (DocumentPage page in result.Pages)
{
    Console.WriteLine($"Lines found on page {page.PageNumber}");
    foreach (var line in page.Lines)
    {
        Console.WriteLine($"  {line.Content}");
    }

    Console.WriteLine($"Selection marks found on page {page.PageNumber}");
    foreach (var selectionMark in page.SelectionMarks)
    {
        Console.WriteLine($"  Selection mark is '{selectionMark.State}' with confidence {selectionMark.Confidence}");
    }
}

// Iterate over the document tables
for (int i = 0; i < result.Tables.Count; i++)
{
    Console.WriteLine($"Table {i + 1}");
    foreach (var cell in result.Tables[i].Cells)
    {
        Console.WriteLine($"  Cell[{cell.RowIndex}][{cell.ColumnIndex}] has content '{cell.Content}' with kind '{cell.Kind}'");
    }
}