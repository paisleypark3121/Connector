using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connector.Test
{
    [TestClass]
    public class NeuroskyConnectorTest
    {
        [TestMethod]
        public void NeuroskyConnectorRealTest()
        {
            #region arrange
            bool expected_getData = true;
            string[] args = new string[] { "COM5" };
            #endregion

            #region act
            IConnector connector = new ThinkgearConnector(args);
            connector.Connect();
            bool actual_getData=(bool)connector.GetData(1);
            float actual_TG_DATA_RAW = (float)connector.GetValue(4);
            float actual_TG_DATA_DELTA = (float)connector.GetValue(5);
            #endregion

            #region assert
            Assert.AreEqual(expected_getData, actual_getData);
            Assert.IsNotNull(actual_TG_DATA_RAW);
            Assert.IsNotNull(actual_TG_DATA_DELTA);
            #endregion
        }
    }
}
