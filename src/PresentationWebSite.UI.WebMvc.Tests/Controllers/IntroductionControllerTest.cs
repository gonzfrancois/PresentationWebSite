using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PresentationWebSite.UI.WebMvc.Models.Introduction;
using System.Runtime.Serialization.Json;

namespace PresentationWebSite.UI.WebMvc.Tests.Controllers
{
    [TestFixture]
    class IntroductionControllerTest
    {
        [Test()]
        public string TestJson()
        {
            var o = new ShowSkillsInChartModel
            {
                ChartDatas = new ChartPie()
                {
                    Infos = new CharPieInfo()
                    {
                        Caption = "My first Pie",
                        SubCaption = "Skills",
                        ShowPlotBorder = 1,
                        PieFillAlpha = 60,
                        Pieborderthickness = 2,
                        Hoverfillcolor = "#CCCCCC",
                        Piebordercolor = "#FFFFFF",
                        Numberprefix = "$",
                        PlotToolText = "$label, $$valueK, $percentValue",
                        Theme = "fint"
                    },
                    Slices = new List<ChartPieSlice>()
                    {
                        new ChartPieSlice()
                        {
                            Label = "Product",
                            Color = "#ffffff",
                            Value = 100,
                            Slices = new List<ChartPieSlice>()
                            {
                                new ChartPieSlice()
                                {
                                    Label = "Food & {br}Beverages",
                                    Color = "#f8bd19",
                                    Value = 25,
                                    ToolTip = "Food & Beverages, $$valueK, $percentValue",
                                    Slices = new List<ChartPieSlice>()
                                    {
                                        new ChartPieSlice()
                                        {
                                            Label = "Bread",
                                            Color = "#f8bd19",
                                            Value = 11.1
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Juice",
                                            Color = "#f8bd19",
                                            Value = 27.5
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Noodles",
                                            Color = "#f8bd19",
                                            Value = 9.99
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Seafood",
                                            Color = "#f8bd19",
                                            Value = 6.6
                                        }
                                    }
                                },
                                new ChartPieSlice()
                                {
                                    Label = "Apparel &{br}Accessories",
                                    Color = "#33ccff",
                                    Value = 25,
                                    ToolTip = "Apparel & Accessories, $$valueK, $percentValue",
                                    Slices = new List<ChartPieSlice>()
                                    {
                                        new ChartPieSlice()
                                        {
                                            Label = "Sun Glasses",
                                            Color = "#33ccff",
                                            Value = 11.1
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Clothing",
                                            Color = "#33ccff",
                                            Value = 27.5
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Handbags",
                                            Color = "#33ccff",
                                            Value = 9.99
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Shoes",
                                            Color = "#33ccff",
                                            Value = 6.6
                                        }
                                    }
                                },
                                new ChartPieSlice()
                                {
                                    Label = "Baby {br}Products",
                                    Color = "#ffcccc",
                                    Value = 25,
                                    ToolTip = "Baby & products, $$valueK, $percentValue",
                                    Slices = new List<ChartPieSlice>()
                                    {
                                        new ChartPieSlice()
                                        {
                                            Label = "Bath &{br}Grooming",
                                            Color = "#33ccff",
                                            Value = 11.1
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Food",
                                            Color = "#33ccff",
                                            Value = 27.5
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Diapers",
                                            Color = "#33ccff",
                                            Value = 6.6
                                        }
                                    }
                                },
                                new ChartPieSlice()
                                {
                                    Label = "Electronics",
                                    Color = "#ccff66",
                                    Value = 25,
                                    ToolTip = "Electronics, $$valueK, $percentValue",
                                    Slices = new List<ChartPieSlice>()
                                    {
                                        new ChartPieSlice()
                                        {
                                            Label = "Tv",
                                            Color = "#33ccff",
                                            Value = 11.1
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "Laptops",
                                            Color = "#33ccff",
                                            Value = 27.5
                                        },
                                        new ChartPieSlice()
                                        {
                                            Label = "SmartPhone",
                                            Color = "#33ccff",
                                            Value = 6.6
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ChartPie));
            ser.WriteObject(stream1,o.ChartDatas);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            return sr.ReadToEnd();
        }
    }
}
