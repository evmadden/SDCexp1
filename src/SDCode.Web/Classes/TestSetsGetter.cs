using System;
using System.Collections.Generic;
using System.Linq;
using SDCode.Web.Models;

namespace SDCode.Web.Classes
{
    public interface ITestSetsGetter
    {
        TestSetsModel Get(string participantID);
    }

    public class TestSetsGetter : ITestSetsGetter
    {
        private readonly ICsvFile<TestSetsModel, TestSetsModel.Map> _testSetsCsvFile;
        private readonly IImageIndexesGetter _imageIndexesGetter;

        public TestSetsGetter(ICsvFile<TestSetsModel, TestSetsModel.Map> testSetsCsvFile, IImageIndexesGetter imageIndexesGetter)
        {
            _testSetsCsvFile = testSetsCsvFile;
            _imageIndexesGetter = imageIndexesGetter;
        }

        public TestSetsModel Get(string participantID)
        {
            var testSets = _testSetsCsvFile.Read().ToList();
            var result = testSets.SingleOrDefault(x => string.Equals(x.ParticipantID, participantID));
            if (result == default)
            {
                Func<IEnumerable<string>> createNewSet = () => _imageIndexesGetter.Get(new List<string> { "E", "EI", "D", "DI", "F", "FI", "N", "NI" });
                var immediate = createNewSet();
                var delayed = createNewSet();
                var followup = createNewSet();
                result = new TestSetsModel { ParticipantID = participantID, Immediate = immediate, Delayed = delayed, Followup = followup };
                testSets.Insert(0, result);
                _testSetsCsvFile.Write(testSets);
            }
            return result;
        }
    }
}
