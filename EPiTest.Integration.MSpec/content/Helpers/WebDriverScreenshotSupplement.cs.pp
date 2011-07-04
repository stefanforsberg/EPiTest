using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Machine.Specifications;

namespace $rootnamespace$.Helpers
{
    public class WebDriverScreenshotSupplement : ISupplementSpecificationResults
    {
        public Result SupplementResult(Result result)
        {
            var driver = UiTestBase.Driver;

            if (driver == null)
            {
                return result;
            }

            if (result.Status != Status.Failing)
            {
                return result;
            }

            var reportPath = System.Configuration.ConfigurationManager.AppSettings["ReportPath"];

            if (!Directory.Exists(reportPath))
            {
                throw new ApplicationException(@"Could not find the path defined in the ReportPath in AppSettings. Make sure that the path exists otherwise you will not get the awesome screenshots in your MSpec report.");
            }

            var path = Path.Combine(reportPath, Guid.NewGuid() + ".png");

            var supplement = new Dictionary<string, string>();

            driver
                .GetScreenshot()
                .SaveAsFile(path, ImageFormat.Png);

            supplement.Add("img-screenshot", path);

            return result.HasSupplement("Selenium") ? result : Result.Supplement(result, "Selenium", supplement);
        }
    }
}
