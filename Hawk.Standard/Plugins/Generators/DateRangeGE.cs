﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Hawk.Standard.Interfaces;
using Hawk.Standard.Utils.Plugins;

namespace Hawk.Standard.Plugins.Generators

{
    [XFrmWork("DateRangeGE", "DateRangeGE_desc","timer_rewind")]
    internal class DateRangeGE : GeneratorBase
    {
        private readonly string staticDateFormat = "yyyy-MM-dd HH:mm:ss:ffff";
        private readonly string staticSpanFormat = "h'h 'm'm 's's'";

        public DateRangeGE()
        {
            Format = staticDateFormat;
            MaxValue = DateTime.Now.ToString(Format);
            MinValue = (DateTime.Now - TimeSpan.FromDays(3)).ToString(Format);
            Interval = TimeSpan.FromHours(1).ToString(staticSpanFormat);
        }
        [BrowsableAttribute.LocalizedCategoryAttribute("key_21")]
        [LocalizedDisplayName("key_375")]
        [LocalizedDescription("key_398")]
        public string MinValue { get; set; }
        [BrowsableAttribute.LocalizedCategoryAttribute("key_21")]
        [LocalizedDisplayName("key_374")]
        [LocalizedDescription("key_398")]
        public string MaxValue { get; set; }
        [BrowsableAttribute.LocalizedCategoryAttribute("key_21")]
        [LocalizedDisplayName("key_399")]
        [LocalizedDescription("key_400")]
        public string Interval { get; set; }
        [BrowsableAttribute.LocalizedCategoryAttribute("key_21")]
        [LocalizedDisplayName("key_401")]
        [LocalizedDescription("key_402")]
        public string Format { get; set; }


        [Browsable(false)]
        public override string KeyConfig => String.Format("min:{0},max:{1}", MinValue, MaxValue);
        public override IEnumerable<IFreeDocument> Generate(IFreeDocument document = null)
        {
            //TODO
            DateTime min, max;
            TimeSpan span;
            if (DateTime.TryParseExact(MinValue,
                staticDateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out min) && DateTime.TryParseExact(MaxValue,
                    staticDateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out max) && TimeSpan.TryParseExact(Interval,
                        staticSpanFormat,
                        CultureInfo.InvariantCulture,
                        TimeSpanStyles.None,
                        out span))

            {
                for (var i = min; i <= max; i += span)
                {
                    var item = new FreeDocument();
                    item.Add(Column, i.ToString(Format));
                    yield return item;
                }
            }
        }
    }

}