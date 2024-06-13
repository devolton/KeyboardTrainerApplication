using System;
using System.Collections.Generic;
using System.Data.Entity;
using CourseProjectKeyboardApplication.Database.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Database.Context
{
    public class TypingTutorDbContextInitializer : CreateDatabaseIfNotExists<TypingTutorDbContext>
    {
        protected override void Seed(TypingTutorDbContext context)
        {
            var levelsCollection = InitLevels();
            var firstLevelLessonsCollection = InitFirstLevelLessons();
            var secondLevelLessonCollection = InitSecondLevelLessons();
            var thirdLevelLessonCollectoin = InitThirdLevelLessons();
            var fourthLevelLessonCollection = InitFourthLevelLessons();
            var fivethLevelLessonCollection = InitFivethLevelLesson();
            var sixhtLevelLessonCollection = InitSixthLevelLesson();
            var seventhLevelLessonCollectoin = InitSeventhLevelLesson();
            var eightLevelLessonCollection = InitEighthLevelLesson();
            var ninethLevelLessonCollection = InitNinethLevelLesson();
            var tenthLevelLessonCollection = InitTenthLevelLessons();
            context.EnglishLayoutLevels.AddRange(levelsCollection);
            context.EnglishLayoutLessons.AddRange(firstLevelLessonsCollection);
            context.EnglishLayoutLessons.AddRange(secondLevelLessonCollection);
            context.EnglishLayoutLessons.AddRange(thirdLevelLessonCollectoin);
            context.EnglishLayoutLessons.AddRange(fourthLevelLessonCollection);
            context.EnglishLayoutLessons.AddRange(fivethLevelLessonCollection);
            context.EnglishLayoutLessons.AddRange(sixhtLevelLessonCollection);
            context.EnglishLayoutLessons.AddRange(seventhLevelLessonCollectoin);
            context.EnglishLayoutLessons.AddRange(eightLevelLessonCollection);
            context.EnglishLayoutLessons.AddRange(ninethLevelLessonCollection);
            context.EnglishLayoutLessons.AddRange(tenthLevelLessonCollection);
        }
        private List<EnglishLayoutLevel> InitLevels()
        {
            return new List<EnglishLayoutLevel>()
            {
                new EnglishLayoutLevel()
                {
                    Id=1,
                    Ordinal =1,
                    Title= "Home Row Position",

                },
                new EnglishLayoutLevel()
                {
                    Id =2,
                    Ordinal =2,
                    Title= "Index Fingers",

                },
                new EnglishLayoutLevel()
                {Id = 3,
                    Ordinal =3,
                    Title= "Middle and Ring Fingers",

                },
                new EnglishLayoutLevel()
                {Id = 4,
                    Ordinal =4,
                    Title= "Pinkies",

                },
                new EnglishLayoutLevel()
                {Id = 5,
                    Ordinal =5,
                    Title= "Repetition 1",

                },
                new EnglishLayoutLevel()
                {Id = 6,
                    Ordinal =6,
                    Title= "Repetition 2",

                },
                new EnglishLayoutLevel()
                {Id = 7,
                    Ordinal =7,
                    Title= "Repetition 3",

                },
                new EnglishLayoutLevel()
                {Id = 8,
                    Ordinal =8,
                    Title= "Repetition 4",

                },
                new EnglishLayoutLevel()
                {Id = 9,
                    Ordinal =9,
                    Title= "Vowels Сryptogram",

                },
                new EnglishLayoutLevel()
                {Id = 10,
                    Ordinal =10,
                    Title= "Alphabet Сryptogram",

                },
                new EnglishLayoutLevel()
                {Id = 11,
                    Ordinal =11,
                    Title= "Common Combinations",

                },
                new EnglishLayoutLevel()
                {Id = 12,
                    Ordinal =12,
                    Title= "Numbers and Punctuation",

                },
                new EnglishLayoutLevel()
                {Id = 13,
                    Ordinal =13,
                    Title= "Using Shift Key",

                },
                new EnglishLayoutLevel()
                {Id = 14,
                    Ordinal =14,
                    Title= "Numbers and Punctuation 2",

                },
                new EnglishLayoutLevel()
                {Id = 15,
                    Ordinal =15,
                    Title= "Sentences",

                },


            };
        }
        private List<EnglishLayoutLesson> InitFirstLevelLessons()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    Text="kkjk kjkkj jkjjk kkjkj jjkjk jkjkk kjkjj jkjkj kkjkj jjkjk",
                    EnglishLayoutLevelId=1,
                    Ordinal=1
                },
                new EnglishLayoutLesson()
                {
                    Text="ffdf fdffd dfddf ffdfd ddfdf dfdff fdfdd dfdfd ffdfd ddfdf",
                    EnglishLayoutLevelId=1,
                    Ordinal=2
                },
                new EnglishLayoutLesson()
                {
                    Text="ffjf jjjff fjffj jjffj fffjj dkdkd kdkdk kkddk ddkdk kddkk",
                    EnglishLayoutLevelId=1,
                    Ordinal=3
                },
                new EnglishLayoutLesson()
                {
                    Text="djjd kkfkk ffjfj kkdkk jjdjd kdkdk ffjjk kkddf dkfdf kkjjf",
                    EnglishLayoutLevelId=1,
                    Ordinal=4
                },
                new EnglishLayoutLesson()
                {
                    Text=";;l; ;l;;l l;ll; ;;l;l ll;l; l;l;; ;l;ll l;l;l ;;l;l ll;l;",
                    EnglishLayoutLevelId=1,
                    Ordinal=5
                },
                new EnglishLayoutLesson()
                {
                    Text=";fk ;fk djlf kflf ;fkj kj;f dj;;f ;f;lf ;lfkf dljdf ;f;lfkf",
                    EnglishLayoutLevelId=1,
                    Ordinal=6
                },
                new EnglishLayoutLesson()
                {
                    Text="ssas sassa asaas ssasa aasas asass sasaa asasa ssasa aasas",
                    EnglishLayoutLevelId=1,
                    Ordinal=7
                },
                new EnglishLayoutLesson()
                {
                    Text="all add; as ask; sad; fas lad dak; dad fad fall; lass dall;",
                    EnglishLayoutLevelId=1,
                    Ordinal=8
                },
                new EnglishLayoutLesson()
                {
                    Text="alas dald fall; dad flak; lass sad; fass; all fall lad; ask",
                    EnglishLayoutLevelId=1,
                    Ordinal=9
                },
                };
        }
        private List<EnglishLayoutLesson> InitSecondLevelLessons()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=1,
                    Text ="ghhgh hghhg ghggh hhghg gghgh ghghh hghgg ghghg hhghg gghgh"
                },
                 new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=2,
                    Text ="gad has aha; had flag gas; sag ash; gag dash glag half;"
                },
                  new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=3,
                    Text ="gaff; hall hald saga hash; sash gall flag; has dash half"
                },
                   new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=4,
                    Text ="rttrt trttr rtrrt ttrtr rrtrt rtrtt trtrr rtrtr ttrtr rrtrt"
                },
                    new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=5,
                    Text ="art tad gar at sat rag tag; far jar tar rah hat rat rag tat"
                },
                     new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=6,
                    Text ="daft dart jars task; hard tart start grad data talk; shaft rash"
                },
                      new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=7,
                    Text ="hart; haft karat halt salt dark; raft draft shark; grass"
                },
                       new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=8,
                    Text ="graft fast raja shark gard shard start lard; flat"
                },
                        new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=9,
                    Text ="yuuyu uyuuy yuyyu uuyuy yyuyu yuyuu uyuyy yuyuy uuyuy yyuyu"
                },
                         new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=10,
                    Text ="day fly dug lay jay sky lug fur fry try hut; say lady yard"
                },
                          new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=11,
                    Text ="lush shut fray surd lurk usual surf flay just lust dust"
                },
                           new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=12,
                    Text ="vbbvb bvbbv vbvvb bbvbv vvbvb vbvbb bvbvv vbvbv bbvbv vvbvb"
                },
                            new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=13,
                    Text ="bad vag bug bar vug vas vat bav bay bat bag bah vast bur"
                },
                             new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=14,
                    Text ="baby vagary burst vary ruby valuta buy vast vault vulgar by"
                },
                              new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=15,
                    Text ="nmmnm mnmmn nmnnm mmnmn nnmnm nmnmm mnmnn nmnmn mmnmn"
                },
                               new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=16,
                    Text ="ham gun jam ran gum man fun mat nab arm sun may nut tun mud"
                },
                                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=17,
                    Text ="hang damm harm darn farm hung lamb rang sand tang tank many"
                },
                                 new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=18,
                    Text ="must bank gang busy hand thank bury junk human marry funny"
                },
                                  new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 2,
                    Ordinal=19,
                    Text ="humb bush ray baulk flask bald stuff bask shark navy hurry"
                }
            };
        }
        private List<EnglishLayoutLesson> InitThirdLevelLessons()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=1,
                    Text="iooio oiooi ioiio ooioi iioio ioioo oioii ioioi ooioi iioio"
            },
                   new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=2,
                    Text="hid ill oil for hit out rid dog hot old too sit oat fin aim"
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=3,
                    Text="weewe eweew wewwe eewew wwewe wewee eweww wewew eewew wwewe"
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=4,
                    Text="red tea way leg tie let see owe wet set lie few how sir who"
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=5,
                    Text=",..,. .,.., ,.,,. ..,., ,,.,. ,.,.. .,.,, ,.,., ..,., ,,.,."
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=6,
                    Text="why, yes. low, two, was, den. led. ebb, ten, you. new, met."
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=7,
                    Text="xccxc cxccx xcxxc ccxcx xxcxc xcxcc cxcxx xcxcx ccxcx xxcxc"
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=8,
                    Text="act, six icy cub fix cab car. wax cry arc axe, can cat cod"
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=9,
                    Text="also take, wake late ease, joke sort food obey under beyond"
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=10,
                    Text="exercise hair rare. gold xenon warry; worm shirt luxury soul"
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=11,
                    Text="after death known; early first jewel large offer raise order"
            },
                new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=12,
                    Text="hotel later ready. agree dirty earth, floor weight water soil"
            },
                 new EnglishLayoutLesson()
            {
                    EnglishLayoutLevelId=3,
                    Ordinal=13,
                    Text="light knife; house lunch naught yield; world story where habit"
            }


            };
        }
        private List<EnglishLayoutLesson> InitFourthLevelLessons()
        {
            return new List<EnglishLayoutLesson>()
            { 
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4, 
                    Ordinal = 1,
                    Text="zqqzq qzqqz zqzzq qqzqz zzqzq zqzqq qzqzz zqzqz qqzqz zzqzq"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 2,
                    Text="zoo quit zit quite zoom quick zest zing quake zeal zany"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 3,
                    Text="p[[p[ [p[[p p[pp[ [[p[p pp[p[ p[p[[ [p[pp p[p[p [[p[p pp[p["
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 4,
                    Text="put opaque [pup puzzle [proposal [prompt plan [pomp lamp"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 5,
                    Text="]]'/ ]'/]' ']'/] /]']' '/]'] ']'/] ]']/' /]']' ]/']' '']/]"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 6,
                    Text="plus/minus; acropolis' [appall] miles/hour [cm/sec] per' pair"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 7,
                    Text="zombie square; poetic /marquee/ question prize [quiz] proper"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 8,
                    Text="poster. price queen [plate] zippy, reply zero km/h quality;"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 9,
                    Text="zigzag' porosity, quantity peace/poker camp zodiac damp plan"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 10,
                    Text="zone quarter. prosperity zirconium' /pound/ power; [press]"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 4,
                    Ordinal = 11,
                    Text="zipper [zoological] quack, piece proud; pearl. penetrate/"
                }

            };
        }
        private List<EnglishLayoutLesson> InitFivethLevelLesson()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=1,
                    Text="mean mean mean mean mean mean mean mean mean mean mean mean"
                },
                 new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=2,
                    Text="jeans jeans jeans jeans jeans jeans jeans jeans jeans jeans"
                },
                  new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=3,
                    Text="echo echo echo echo echo echo echo echo echo echo echo echo"
                },
                   new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=4,
                    Text="thin thin thin thin thin thin thin thin thin thin thin thin"
                },
                    new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=5,
                    Text="disk disk disk disk disk disk disk disk disk disk disk disk"
                },
                     new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=6,
                    Text="dish dish dish dish dish dish dish dish dish dish dish dish"
                    
                },
                      new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=7,
                    Text="dale dale dale dale dale dale dale dale dale dale dale dale"
                },
                       new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=8,
                    Text="oils oils oils oils oils oils oils oils oils oils oils oils"
                },
                        new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=9,
                    Text="path path path path path path path path path path path path"
                },
                         new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=10,
                    Text="last last last last last last last last last last last last"
                },
                          new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=11,
                    Text="land land land land land land land land land land land land"
                },
                           new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=12,
                    Text="pets pets pets pets pets pets pets pets pets pets pets pets"
                },
                            new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=13,
                    Text="mean jeans echo thin disk dish dale oils path last land pets"
                },
                             new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=5,
                    Ordinal=14,
                    Text="mean jeans echo thin disk dish dale oils path last land pets"
                }


            };
        }
        private List<EnglishLayoutLesson> InitSixthLevelLesson()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 1, 
                    Text="pound pound pound pound pound pound pound pound pound pound"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 2,
                    Text="pits pits pits pits pits pits pits pits pits pits pits pits"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 3,
                    Text="ryas ryas ryas ryas ryas ryas ryas ryas ryas ryas ryas ryas"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 4,
                    Text="ebbed ebbed ebbed ebbed ebbed ebbed ebbed ebbed ebbed ebbed"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 5,
                    Text="risk risk risk risk risk risk risk risk risk risk risk risk"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 6,
                    Text="reeks reeks reeks reeks reeks reeks reeks reeks reeks reeks"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 7,
                    Text="leak leak leak leak leak leak leak leak leak leak leak leak"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 8,
                    Text="lens lens lens lens lens lens lens lens lens lens lens lens"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 9,
                    Text="flux flux flux flux flux flux flux flux flux flux flux flux"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 10,
                    Text="leave leave leave leave leave leave leave leave leave leave leave leave"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 11,
                    Text="leis leis leis leis leis leis leis leis leis leis leis leis"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 12,
                    Text="leaf leaf leaf leaf leaf leaf leaf leaf leaf leaf leaf leaf"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 13,
                    Text="pound pits ryas ebbed risk reeks leak lens flux leave leis leaf"
                },
                 new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =6,
                    Ordinal = 14,
                    Text="pound pits ryas ebbed risk reeks leak lens flux leave leis leaf"
                }

            };
        }
        private List<EnglishLayoutLesson> InitSeventhLevelLesson()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=1,
                    Text ="lief lief lief lief lief lief lief lief lief lief lief lief"
                },
                 new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=2,
                    Text ="lazy lazy lazy lazy lazy lazy lazy lazy lazy lazy lazy lazy"
                },
                  new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=3,
                    Text ="keno keno keno keno keno keno keno keno keno keno keno keno"
                },
                   new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=4,
                    Text ="quack quack quack quack quack quack quack quack quack quack"
                },
                    new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=5,
                    Text ="knife knife knife knife knife knife knife knife knife knife"
                },

                     new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=6,
                    Text ="jack jack jack jack jack jack jack jack jack jack jack jack"
                },
                      new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=7,
                    Text ="chap chap chap chap chap chap chap chap chap chap chap chap"
                },
                 
                        new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=8,
                    Text ="sock sock sock sock sock sock sock sock sock sock sock sock"
                },
                         new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=9,
                    Text ="keys keys keys keys keys keys keys keys keys keys keys keys"
                },
                          new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=10,
                    Text ="obey obey obey obey obey obey obey obey obey obey obey obey"
                },
                           new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=11,
                    Text ="men's men's men's men's men's men's men's men's men's men's"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=12,
                    Text ="caps caps caps caps caps caps caps caps caps caps caps caps"
                },
                            new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=13,
                    Text ="lief lazy keno quack knife jack chap sock keys obey men's caps"
                },
                     new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 7,
                    Ordinal=14,
                    Text ="lief lazy keno quack knife jack chap sock keys obey men's caps"
                },

            };
        }
        private List<EnglishLayoutLesson> InitEighthLevelLesson()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =1,
                    Text="vile vile vile vile vile vile vile vile vile vile vile vile"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =2,
                    Text="fine fine fine fine fine fine fine fine fine fine fine fine"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =3,
                    Text="vent vent vent vent vent vent vent vent vent vent vent vent"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =4,
                    Text="vale vale vale vale vale vale vale vale vale vale vale vale"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =5,
                    Text="back back back back back back back back back back back back"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =6,
                    Text="bans bans bans bans bans bans bans bans bans bans bans bans"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =7,
                    Text="bags bags bags bags bags bags bags bags bags bags bags bags"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =8,
                    Text="tins tins tins tins tins tins tins tins tins tins tins tins"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =9,
                    Text="gift gift gift gift gift gift gift gift gift gift gift gift"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =10,
                    Text="grit grit grit grit grit grit grit grit grit grit grit grit"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =11,
                    Text="herb herb herb herb herb herb herb herb herb herb herb herb"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =12,
                    Text="hand hand hand hand hand hand hand hand hand hand hand hand"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =13,
                    Text="vile fine vent vale back bans bags tins gift grit herb hand"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=8,
                    Ordinal =14,
                    Text="vile fine vent vale back bans bags tins gift grit herb hand"
                }
            };
        }
        private List<EnglishLayoutLesson> InitNinethLevelLesson()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =9,
                    Ordinal=1,
                    Text="ouoie uaueo iyaei yoeia eyaie uaoyi oyaey iyoeo iouya eaiyu"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =9,
                    Ordinal=2,
                    Text="ieyoi auyei oeaui eyaey oyuae eyoei uyieo aeoyi yioae oiyeu"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =9,
                    Ordinal=3,
                    Text="aeoua ieyoa uaeoe iaeoa ueaya aeyoi uaeoy eioae uaeya ioeia"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =9,
                    Ordinal=4,
                    Text="oeiau yoeie ieaua ieyei auaeo yoieu aeyoi auioy eaiae uoaie"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId =9,
                    Ordinal=5,
                    Text="oyeao ieauy ioeya aueai oaeya iueie yoeau eioei aeyei iyuoa"
                }
            };
        }
        private List<EnglishLayoutLesson> InitTenthLevelLessons()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 10,
                    Ordinal =1,
                    Text="gowof hrocj ayxle rfkqk dugpw cjxln dma]e xjnup skxnz rmokl"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 10,
                    Ordinal =2,
                    Text="aixle rlnlb dmywg tvprh lumtk ajrmw heomc zlnuk pfpex ndlyv"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 10,
                    Ordinal =3,
                    Text="nsptn bwitk zopsw vkstg mdibw auvle quvkn smrkx nithd krihx"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 10,
                    Ordinal =4,
                    Text="odmyl dlekh xutnv cmdiw lwubr hcken amrug ltnxp kwoby cysna"
                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId = 10,
                    Ordinal =5,
                    Text="kruxm aodnw ylq]s kpayc xuspg nzkej iehxf krmxl hsitb dmysk"
                },
            };
        }
    }
}