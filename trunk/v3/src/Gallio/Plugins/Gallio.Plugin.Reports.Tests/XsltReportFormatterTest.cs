using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using Gallio.Core.ProgressMonitoring;
using Gallio.Hosting;
using Gallio.Logging;
using Gallio.Runner.Reports;
using Gallio.Tests;
using MbUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;

namespace Gallio.Plugin.Reports.Tests
{
    [TestFixture]
    [TestsOn(typeof(XsltReportFormatter))]
    public class XsltReportFormatterTest : BaseUnitTest
    {
        private delegate void SerializeReportDelegate(XmlWriter writer, ExecutionLogAttachmentContentDisposition contentDisposition);

        [Test, ExpectedArgumentNullException]
        public void RuntimeCannotBeNull()
        {
            new XsltReportFormatter(null, "SomeName", "ext", "file://content", "xslt", new string[0]);
        }

        [Test, ExpectedArgumentNullException]
        public void NameCannotBeNull()
        {
            new XsltReportFormatter(Mocks.Stub<IRuntime>(), null, "ext", "file://content", "xslt", new string[0]);
        }

        [Test, ExpectedArgumentNullException]
        public void ExtensionCannotBeNull()
        {
            new XsltReportFormatter(Mocks.Stub<IRuntime>(), "SomeName", null, "file://content", "xslt", new string[0]);
        }

        [Test, ExpectedArgumentNullException]
        public void ContentPathCannotBeNull()
        {
            new XsltReportFormatter(Mocks.Stub<IRuntime>(), "SomeName", "ext", null, "xslt", new string[0]);
        }

        [Test, ExpectedArgumentNullException]
        public void XsltPathCannotBeNull()
        {
            new XsltReportFormatter(Mocks.Stub<IRuntime>(), "SomeName", "ext", "file://content", null, new string[0]);
        }

        [Test, ExpectedArgumentNullException]
        public void ResourcePathsCannotBeNull()
        {
            new XsltReportFormatter(Mocks.Stub<IRuntime>(), "SomeName", "ext", "file://content", "xslt", null);
        }

        [Test, ExpectedArgumentNullException]
        public void ResourcePathsCannotContainNulls()
        {
            new XsltReportFormatter(Mocks.Stub<IRuntime>(), "SomeName", "ext", "file://content", "xslt", new string[] { null });
        }

        [Test]
        public void NameIsTheSameAsWasSpecifiedInTheConstructor()
        {
            XsltReportFormatter formatter = new XsltReportFormatter(Mocks.Stub<IRuntime>(), "SomeName", "ext", "file://content", "xslt", new string[] { "res1", "res2" });
            Assert.AreEqual("SomeName", formatter.Name);
        }

        [Test]
        public void TheDefaultAttachmentContentDispositionIsAbsent()
        {
            XsltReportFormatter formatter = new XsltReportFormatter(Mocks.Stub<IRuntime>(), "SomeName", "ext", "file://content", "xslt", new string[] { "res1", "res2" });
            Assert.AreEqual(ExecutionLogAttachmentContentDisposition.Absent, formatter.DefaultAttachmentContentDisposition);
        }

        [Test]
        public void TheDefaultAttachmentContentDispositionCanBeChanged()
        {
            XsltReportFormatter formatter = new XsltReportFormatter(Mocks.Stub<IRuntime>(), "SomeName", "ext", "file://content", "xslt", new string[] { "res1", "res2" });

            formatter.DefaultAttachmentContentDisposition = ExecutionLogAttachmentContentDisposition.Inline;
            Assert.AreEqual(ExecutionLogAttachmentContentDisposition.Inline, formatter.DefaultAttachmentContentDisposition);
        }

        [Test]
        public void FormatWritesTheTransformedReport()
        {
            string resourcePath = Path.Combine(Path.GetDirectoryName(Loader.GetAssemblyLocalPath(GetType().Assembly)), @"..\Resources");

            IRuntime runtime = Mocks.CreateMock<IRuntime>();
            IReportWriter reportWriter = Mocks.CreateMock<IReportWriter>();
            IReportContainer reportContainer = Mocks.CreateMock<IReportContainer>();
            IProgressMonitor progressMonitor = NullProgressMonitor.CreateInstance();

            string reportPath = Path.GetTempFileName();
            using (Stream tempFileStream = File.OpenWrite(reportPath))
            {
                using (Mocks.Record())
                {
                    SetupResult.For(reportWriter.ReportContainer).Return(reportContainer);

                    Expect.Call(runtime.MapUriToLocalPath(null))
                        .Constraints(Is.Equal(new Uri("file://content")))
                        .Return(resourcePath);

                    reportWriter.SerializeReport(null, ExecutionLogAttachmentContentDisposition.Link);
                    LastCall.Constraints(Is.NotNull(), Is.Equal(ExecutionLogAttachmentContentDisposition.Link))
                        .Do((SerializeReportDelegate)delegate(XmlWriter writer, ExecutionLogAttachmentContentDisposition contentDisposition)
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.InnerXml = "<report>The report.</report>";
                            doc.Save(writer);
                        });

                    SetupResult.For(reportContainer.ReportName).Return("Foo");
                    Expect.Call(reportContainer.OpenReportFile("Foo.ext", FileMode.Create, FileAccess.Write))
                        .Return(tempFileStream);
                    reportWriter.AddReportDocumentPath("Foo.ext");

                    reportContainer.CopyToReport(Path.Combine(resourcePath, "res1"), @"Foo\res1");
                    reportContainer.CopyToReport(Path.Combine(resourcePath, "res2"), @"Foo\res2");

                    reportWriter.SaveReportAttachments(null);
                    LastCall.Constraints(Is.NotNull());
                }

                using (Mocks.Playback())
                {
                    XsltReportFormatter formatter = new XsltReportFormatter(runtime, "SomeName", "ext", "file://content", "Diagnostic.xslt", new string[] { "res1", "res2" });
                    NameValueCollection options = new NameValueCollection();
                    options.Add(XsltReportFormatter.AttachmentContentDispositionOption, ExecutionLogAttachmentContentDisposition.Link.ToString());

                    formatter.Format(reportWriter, options, progressMonitor);

                    string reportContents = File.ReadAllText(reportPath);
                    Log.EmbedXml("Diagnostic report contents", reportContents);
                    Assert.Contains(reportContents, "<resourceRoot>Foo</resourceRoot>");
                    Assert.Contains(reportContents, "The report.");

                    File.Delete(reportPath);
                }
            }
        }
    }
}