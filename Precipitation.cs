using System;
using System.Runtime.Serialization;

namespace OOP_Project
{
    [Serializable][DataContract]
    public class Precipitation
    {
        [DataMember]
        private string _descClassPrecIntEn;
        [DataMember]
        private string _descClassPrecIntPt;
        [DataMember]
        private string _classPrecInt;
        
        public Precipitation(){}

        public Precipitation(string descClassPrecIntEn, string descClassPrecIntPt, string classPrecInt)
        {
            _descClassPrecIntEn = descClassPrecIntEn;
            _descClassPrecIntPt = descClassPrecIntPt;
            _classPrecInt = classPrecInt;
        }

        public string GetDescClassPrecIntEn()
        {
            return _descClassPrecIntEn;
        }

        public void SetDescClassPrecIntEn(string descClassPrecIntEn)
        {
            _descClassPrecIntEn = descClassPrecIntEn;
        }
        
        public string GetDescClassPrecIntPt()
        {
            return _descClassPrecIntPt;
        }

        public void SetDescClassPrecIntPt(string descClassPrecIntPt)
        {
            _descClassPrecIntPt = descClassPrecIntPt;
            
        }
        public string GetClassPrecInt()
        {
            return _classPrecInt;
        }

        public void SetClassPrecInt(string classPrecInt)
        {
            _classPrecInt = classPrecInt;
        }

    }
}