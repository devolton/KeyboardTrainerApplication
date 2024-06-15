using System;
using System.Collections.Generic;
using System.Data.Entity;
using CourseProjectKeyboardApplication.Database.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyboardApplicationToolsLibrary.AuthorizationTools;
using System.Windows;

namespace CourseProjectKeyboardApplication.Database.Context
{
    public class TypingTutorDbContextInitializer : CreateDatabaseIfNotExists<TypingTutorDbContext>
    {
        
        protected override void Seed(TypingTutorDbContext context)
        {
            try
            {
                //levels
                var levelsCollection = InitLevels();


                //lessons
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
                var eleventhLevelLessonsCollection = InitEleventhLevelLesson();
                var twelvethLevelLessonsCollection = InitTwelvethLevelLessons();
                var thirteenthLevelLessonsCollection = InitThirteenthLevelLessons();
                var fourteenthLevelLessonsCollection = InitFourteenthLevelLessons();
                var fiveteenthLevelLessonsCollection = InitFiveteenthLevelLessons();

                //users
                var usersCollection = InitUser();

                //tests
                var testsCollection = InitTests();

                //educProgresses
                var educProgressCollection = InitEducProgresses();


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
                context.EnglishLayoutLessons.AddRange(eleventhLevelLessonsCollection);
                context.EnglishLayoutLessons.AddRange(twelvethLevelLessonsCollection);
                context.EnglishLayoutLessons.AddRange(thirteenthLevelLessonsCollection);
                context.EnglishLayoutLessons.AddRange(fourteenthLevelLessonsCollection);
                context.EnglishLayoutLessons.AddRange(fiveteenthLevelLessonsCollection);

                context.Users.AddRange(usersCollection);

                context.TypingTestResults.AddRange(testsCollection);

                context.EducationUsersProgresses.AddRange(educProgressCollection);
                context.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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
                    Id= 1,
                    Text="kkjk kjkkj jkjjk kkjkj jjkjk jkjkk kjkjj jkjkj kkjkj jjkjk",
                    EnglishLayoutLevelId=1,
                    Ordinal=1
                },
                new EnglishLayoutLesson()
                {
                    Id= 2,
                    Text="ffdf fdffd dfddf ffdfd ddfdf dfdff fdfdd dfdfd ffdfd ddfdf",
                    EnglishLayoutLevelId=1,
                    Ordinal=2
                },
                new EnglishLayoutLesson()
                {
                    Id= 3,
                    Text="ffjf jjjff fjffj jjffj fffjj dkdkd kdkdk kkddk ddkdk kddkk",
                    EnglishLayoutLevelId=1,
                    Ordinal=3
                },
                new EnglishLayoutLesson()
                {
                    Id=4,
                    Text="djjd kkfkk ffjfj kkdkk jjdjd kdkdk ffjjk kkddf dkfdf kkjjf",
                    EnglishLayoutLevelId=1,
                    Ordinal=4
                },
                new EnglishLayoutLesson()
                {Id = 5,
                    Text=";;l; ;l;;l l;ll; ;;l;l ll;l; l;l;; ;l;ll l;l;l ;;l;l ll;l;",
                    EnglishLayoutLevelId=1,
                    Ordinal=5
                },
                new EnglishLayoutLesson()
                {Id = 6,
                    Text=";fk ;fk djlf kflf ;fkj kj;f dj;;f ;f;lf ;lfkf dljdf ;f;lfkf",
                    EnglishLayoutLevelId=1,
                    Ordinal=6
                },
                new EnglishLayoutLesson()
                {Id = 7,
                    Text="ssas sassa asaas ssasa aasas asass sasaa asasa ssasa aasas",
                    EnglishLayoutLevelId=1,
                    Ordinal=7
                },
                new EnglishLayoutLesson()
                {Id = 8,
                    Text="all add; as ask; sad; fas lad dak; dad fad fall; lass dall;",
                    EnglishLayoutLevelId=1,
                    Ordinal=8
                },
                new EnglishLayoutLesson()
                {Id = 9,        
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
                    Id=10,
                    EnglishLayoutLevelId = 2,
                    Ordinal=1,
                    Text ="ghhgh hghhg ghggh hhghg gghgh ghghh hghgg ghghg hhghg gghgh"
                },
                 new EnglishLayoutLesson()
                {
                     Id=11,
                    EnglishLayoutLevelId = 2,
                    Ordinal=2,
                    Text ="gad has aha; had flag gas; sag ash; gag dash glag half;"
                },
                  new EnglishLayoutLesson()
                {
                      Id=12,
                    EnglishLayoutLevelId = 2,
                    Ordinal=3,
                    Text ="gaff; hall hald saga hash; sash gall flag; has dash half"
                },
                   new EnglishLayoutLesson()
                {
                       Id=13,   
                    EnglishLayoutLevelId = 2,
                    Ordinal=4,
                    Text ="rttrt trttr rtrrt ttrtr rrtrt rtrtt trtrr rtrtr ttrtr rrtrt"
                },
                    new EnglishLayoutLesson()
                {
                        Id=14,
                    EnglishLayoutLevelId = 2,
                    Ordinal=5,
                    Text ="art tad gar at sat rag tag; far jar tar rah hat rat rag tat"
                },
                     new EnglishLayoutLesson()
                {
                         Id=15,
                    EnglishLayoutLevelId = 2,
                    Ordinal=6,
                    Text ="daft dart jars task; hard tart start grad data talk; shaft rash"
                },
                      new EnglishLayoutLesson()
                {
                          Id=16,
                    EnglishLayoutLevelId = 2,
                    Ordinal=7,
                    Text ="hart; haft karat halt salt dark; raft draft shark; grass"
                },
                       new EnglishLayoutLesson()
                {
                           Id=17,
                    EnglishLayoutLevelId = 2,
                    Ordinal=8,
                    Text ="graft fast raja shark gard shard start lard; flat"
                },
                        new EnglishLayoutLesson()
                {
                            Id=18,
                    EnglishLayoutLevelId = 2,
                    Ordinal=9,
                    Text ="yuuyu uyuuy yuyyu uuyuy yyuyu yuyuu uyuyy yuyuy uuyuy yyuyu"
                },
                         new EnglishLayoutLesson()
                {
                             Id=19,
                    EnglishLayoutLevelId = 2,
                    Ordinal=10,
                    Text ="day fly dug lay jay sky lug fur fry try hut; say lady yard"
                },
                          new EnglishLayoutLesson()
                {
                              Id=20,
                    EnglishLayoutLevelId = 2,
                    Ordinal=11,
                    Text ="lush shut fray surd lurk usual surf flay just lust dust"
                },
                           new EnglishLayoutLesson()
                {
                               Id=21,
                    EnglishLayoutLevelId = 2,
                    Ordinal=12,
                    Text ="vbbvb bvbbv vbvvb bbvbv vvbvb vbvbb bvbvv vbvbv bbvbv vvbvb"
                },
                            new EnglishLayoutLesson()
                {
                                Id=22,
                    EnglishLayoutLevelId = 2,
                    Ordinal=13,
                    Text ="bad vag bug bar vug vas vat bav bay bat bag bah vast bur"
                },
                             new EnglishLayoutLesson()
                {   Id=23,
                    EnglishLayoutLevelId = 2,
                    Ordinal=14,
                    Text ="baby vagary burst vary ruby valuta buy vast vault vulgar by"
                },
                              new EnglishLayoutLesson()
                {
                                  Id=24,
                    EnglishLayoutLevelId = 2,
                    Ordinal=15,
                    Text ="nmmnm mnmmn nmnnm mmnmn nnmnm nmnmm mnmnn nmnmn mmnmn"
                },
                               new EnglishLayoutLesson()
                {   Id=25,
                    EnglishLayoutLevelId = 2,
                    Ordinal=16,
                    Text ="ham gun jam ran gum man fun mat nab arm sun may nut tun mud"
                },
                                new EnglishLayoutLesson()
                {   Id=26,
                    EnglishLayoutLevelId = 2,
                    Ordinal=17,
                    Text ="hang damm harm darn farm hung lamb rang sand tang tank many"
                },
                      new EnglishLayoutLesson()
                {   Id=27,
                    EnglishLayoutLevelId = 2,
                    Ordinal=18,
                    Text ="must bank gang busy hand thank bury junk human marry funny"
                },
                                  new EnglishLayoutLesson()
                {   Id=29,
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
        private List<EnglishLayoutLesson> InitEleventhLevelLesson()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 1,
                    Text="ing ing ing ing ing ing ing ing ing ing ing ing ing"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 2,
                    Text="tion tion tion tion tion tion tion tion tion tion tion"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 3,
                    Text="ment ment ment ment ment ment ment ment ment ment ment"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 4,
                    Text="ure ure ure ure ure ure ure ure ure ure ure ure ure ure"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 5,
                    Text="sion sion sion sion sion sion sion sion sion sion sion"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 6,
                    Text="ous ous ous ous ous ous ous ous ous ous ous ous ous ous"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 7,
                    Text="our our our our our our our our our our our our our our"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 8,
                    Text="er or er or er or er or er or er or er or er or er or"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 9,
                    Text="tch tch tch tch tch tch tch tch tch tch tch tch tch tch"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 10,
                    Text="ck ch ck ch ck ch ck ch ck ch ck ch ck ch ck ch ck ch ck"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 11,
                    Text="ea ea ea ea ea ea ea ea ea ea ea ea ea ea ea ea ea ea ea"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 12,
                    Text="er er er er er er er er er er er er er er er er er er er"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 13,
                    Text="the ht the ht the ht the ht the ht the ht the ht the ht the"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 14,
                    Text="ghe ht the ht the ht the ht the ht the ht the ht the ht the"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 15,
                    Text="es ly es ly es ly es ly es ly es ly es ly es ly es ly es ly"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 16,
                    Text="ed ed ed ed ed ed ed ed ed ed ed ed ed ed ed ed ed ed ed"
                },new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 11,
                    Ordinal = 17,
                    Text="and and and and and and and and and and and and and and"
                }
            };
        }
        private List<EnglishLayoutLesson> InitTwelvethLevelLessons()
        {
            return new List<EnglishLayoutLesson>()
            {
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=1,
                    Text="78878 87887 78778 88787 77878 78788 87877 78787 88787 77878"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=2,
                    Text="mean7 jeans8 echo7 thin8 disk7 dish8 dale7 oils8 path7 last8"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=3,
                    Text="56656 65665 56556 66565 55656 56566 65655 56565 66565 55656"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=4,
                    Text="land5 pets6 pound8 pits7 ryas5 ebbed6 risk5 reeks7 leak8 lens6"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=5,
                    Text="00090 09009 90990 00909 99090 90900 09099 90909 00909 99090"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=6,
                    Text="flux9 lave0 leis7 leaf0 lief6 lazy9 keno5 quack9 knife8 jack0"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=7,
                    Text="34434 43443 34334 44343 33434 34344 43433 34343 44343 33434"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=8,
                    Text="chap3 sock4 keys3 obey0 men's7 caps4 vile3 fine4 vent6 vale4"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=9,
                    Text="-==-= =-==- -=--= ==-=- --=-= -=-== =-=-- -=-=- ==-=- --=-="

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=10,
                    Text="back= bans- bags3 tins- gift4 grit= herb5 hand= pink7 chin="

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=11,
                    Text="12212 21221 12112 22121 11212 12122 21211 12121 22121 11212"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=12,
                    Text="cash2 come1 earn= evil2 form- join3 vote1 deck6 fern2 zeal1"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=13,
                    Text="only6 pain3 sale3 rank0===king7 5bait5 3 5deny5 3 4find42-10"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=14,
                    Text="17-5=12 8-3=5 10-3=7 6-4=2 13-2=11 19-4=15 8-5=3 5-1=4 19-12=7"

                },
                new EnglishLayoutLesson()
                {
                    EnglishLayoutLevelId=12,
                    Ordinal=15,
                    Text="49-1=48 12-3=9 15-9=6 12-8=4 11-4=7 19-13=6 12-3=9 10-6=4 9-4=5"

                }


            };
        }
        private List<EnglishLayoutLesson> InitThirteenthLevelLessons()
        {
            return new List<EnglishLayoutLesson>
            {
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 13,
                    Ordinal =1,
                    Text ="GHDJY GYDUC Z:CCH HGXCD V:JPR JGKXG JDJEG YLG<N JENGT CJGCY"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 13,
                    Ordinal =2,
                    Text ="GTY<D FGYDH KYNUN RDA:G F<DUJ LPDUY RLCPF GCGGD YAFCJ YGEXR"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 13,
                    Ordinal =3,
                    Text ="RPVDC D<\"DV BVXCN TWYPY DNBTG AGLYV RFKBD HYPVL YRPJC G<H:X"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 13,
                    Ordinal =4,
                    Text ="MAGIC RIVER MONEY NOISE PAPER WOMAN EVENT ANGRY COVER METAL"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 13,
                    Ordinal =5,
                    Text ="COMPANY HORSES HAPPY SPECIAL VENTURE CHANGE RETURN KNIGHT"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 13,
                    Ordinal =6,
                    Text ="MagiC RiveR MoneY NoisE PapeR WomaN EvenT AngrY CoveR MetaL"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 13,
                    Ordinal =7,
                    Text ="ComPanY HorSeS HappY SpeCiaL VenTurE ChaNgE RetUrN KniGhT"
                }
            };
        }
        private List<EnglishLayoutLesson> InitFourteenthLevelLessons()
        {
            return new List<EnglishLayoutLesson>
            {
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =1,
                    Text="! @! @ !@! @ ! @!! ! @! @@ !@@ @ ! !@ @ !!@ !@ ! @! @ @! !@@"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =2,
                    Text="@song ! song @! song @ song !@ song ! song @@! song @ song !!"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =3,
                    Text="# $# #$$ $# $#$ # $ $ ##$ # $# $ #$# # $#$ #$ $ # $$# $ #$$"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =4,
                    Text="rain $ rain #$ rain ##$ rain # rain $# rain $ rain $# rain $"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =5,
                    Text="%^ ^ %%^ % ^% ^ %^% ^^ % ^%^ ^ %^ % ^%% ^% ^ %^^ % ^^% % ^%^"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =6,
                    Text="pale %^ pale ^ pale ^%^ pale % pale %^ pale ^% pale ^ pale %"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =7,
                    Text="& *& * &*& * *&& & *& ** &** & * &* * &&* &* & *& * *& &* **"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =8,
                    Text="calf & calf *& calf * calf &*& calf & calf **& calf * calf &"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =9,
                    Text="() ) ( ))( ( )( ()( ) () ))( ( ) ))( )( ) (() )( ( )() () (("
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =10,
                    Text="mask ( mask )( mask) mask ( mask )() mask ) mask () mask )("
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =11,
                    Text="+ _ _+_ _+ _ __+ +_+ _ ++ _+ + _ ++_ _ + +_+ _ +_ + _+ + _+_"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =12,
                    Text="mine _ mine +_ mine _+_ mine + mine _+ mine + mine _ mine _+"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =13,
                    Text="coal (@ coal ?# coal ) coal $^ coal #_ coal )% coal & coal ^"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =14,
                    Text="face $ face #( face $% face * face ^_ face @ face ) face %&@"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =15,
                    Text="deal *$ deal )# deal ( deal &? deal %^ deal &_ deal $ deal @"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =16,
                    Text="1. Mean, 2. Jeans, 3. Echo? 4. Thin, 5. Disk? 6. Dish, 7. Last? 8. Oils, 9. Path"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =17,
                    Text=$"1) 'Pound'; 2) 'Pits'; 3) 'Ryas'; 4) 'Ebbed'; 5) 'Risk'; 6) 'Reeks'; 7) 'Leak'"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =18,
                    Text="(Lief - Lazy) + (Keno ? Quack) + (Knife * Jack) - (Chap & Sock) * (Keys + Obey)"
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId = 14,
                    Ordinal =19,
                    Text="<Vile & Fine> @ <Vent & Vale> @ <Back & Bans> @ <Bags & Tins> @ <Gift>"
                }
            };
        }
        private List<EnglishLayoutLesson> InitFiveteenthLevelLessons()
        {
            return new List<EnglishLayoutLesson>
            {
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=1,
                    Text="1. A bad workman quarrels with his tools."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=2,
                    Text="2. Better a glorious death than a shameful life."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=3,
                    Text="3. Calamity is man's true touchstone."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=4,
                    Text="4. Eat at pleasure, drink with measure."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=5,
                    Text="5. He who pleased everybody died before he was born."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=6,
                    Text="6. Jack of all trades and master of none."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=7,
                    Text="7. Keep a thing seven years and you will find a use for it."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=8,
                    Text="8. Make hay while the sun shine."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=9,
                    Text="9. Between two stools one falls to the ground."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=10,
                    Text="10. Roll my log and I will roll yours."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=11,
                    Text="11. Scornful dogs will eat dirty puddings."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=12,
                    Text="12. We never know the value of water till the well is dry."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=13,
                    Text="13. Old birds are not to be caught with chaff."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=14,
                    Text="14. Zeal without knowledge is a runaway horse."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=15,
                    Text="15. Better die standing than live kneeling."
                },
                new EnglishLayoutLesson
                {
                    EnglishLayoutLevelId =15,
                    Ordinal=16,
                    Text="Looks like I'm typing a bit faster now. Now it's a good time to take a certification test."
                }

            };
        }
        private List<User> InitUser()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Anton",
                    Email = "voloshko.03@gmail.com",
                    Login = "Devolton",
                    Password = PasswordSHA256Encrypter.EncryptPassword("Password123"),
                    EnglishLayoutLessonId = 12,
                    EnglishLayoutLevelId = 2,
                    AvatarPath = ""
                },
                new User
                {
                    Id=2,
                    Name="Artur",
                    Email="volos.02@gmail.com",
                    Login="Arturidze",
                    Password=PasswordSHA256Encrypter.EncryptPassword("Qwerty12345"),
                    EnglishLayoutLessonId=1,
                    EnglishLayoutLevelId=1,
                    AvatarPath=""
                }
            };
        }
        private List<TypingTestResult> InitTests()
        {
            return new List<TypingTestResult>
            {
                new TypingTestResult
                {
                    Id=1,
                    Date = new DateTime(2024,06,10),
                    Speed=40,
                    AccuracyPercent=89.5,
                    LayoutType=Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=2,
                    Date= new DateTime(2024,06,11),
                    Speed=45,
                    AccuracyPercent=90.5,
                    LayoutType=Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=3,
                    Date = new DateTime(2024,05,29),
                    Speed=42,
                    AccuracyPercent = 78.5,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=2
                },
                //
                new TypingTestResult
                {
                    Id=4,
                    Date = new DateTime(2024,06,02),
                    Speed=41,
                    AccuracyPercent = 88.5,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=5,
                    Date = new DateTime(2024,05,22),
                    Speed=39,
                    AccuracyPercent = 98.5,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=6,
                    Date = new DateTime(2024,05,12),
                    Speed=44,
                    AccuracyPercent = 90,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=7,
                    Date = new DateTime(2024,06,07),
                    Speed=47,
                    AccuracyPercent = 80.2,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=8,
                    Date = new DateTime(2024,05,27),
                    Speed=44,
                    AccuracyPercent = 91.8,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=9,
                    Date = new DateTime(2024,06,12),
                    Speed=51,
                    AccuracyPercent = 89.5,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=2
                },
                new TypingTestResult
                {
                    Id=10,
                    Date = new DateTime(2024,06,01),
                    Speed=41,
                    AccuracyPercent = 88.5,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=11,
                    Date = new DateTime(2024,05,29),
                    Speed=46,
                    AccuracyPercent = 93.5,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=12,
                    Date = new DateTime(2024,05,12),
                    Speed=50,
                    AccuracyPercent = 90,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },new TypingTestResult
                {
                    Id=13,
                    Date = new DateTime(2024,05,08),
                    Speed=42,
                    AccuracyPercent = 98,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },
                new TypingTestResult
                {
                    Id=14,
                    Date = new DateTime(2024,05,23),
                    Speed=45,
                    AccuracyPercent = 91.2,
                    LayoutType= Shared.Enums.LayoutType.English,
                    UserId=1
                },

            };
        }
        private List<EducationUsersProgress> InitEducProgresses()
        {
            return new List<EducationUsersProgress>()
            {
                new EducationUsersProgress
                {
                    Id=1,
                    UserId=1,
                    EnglishLayoutLessonId=1,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=true,
                    IsSpeedCompleted=false,
                    IsWithoutErrorsCompleted=false
                },
                new EducationUsersProgress
                {
                    Id=2,
                    UserId=1,
                    EnglishLayoutLessonId=2,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=false,
                    IsSpeedCompleted=true,
                    IsWithoutErrorsCompleted=false
                },
                new EducationUsersProgress
                {
                    Id=3,
                    UserId=1,
                    EnglishLayoutLessonId=3,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=false,
                    IsSpeedCompleted=false,
                    IsWithoutErrorsCompleted=false
                },
                new EducationUsersProgress
                {
                    Id=4,
                    UserId=1,
                    EnglishLayoutLessonId=4,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=true,
                    IsSpeedCompleted=true,
                    IsWithoutErrorsCompleted=true
                },
                new EducationUsersProgress
                {
                    Id=5,
                    UserId=1,
                    EnglishLayoutLessonId=5,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=false,
                    IsSpeedCompleted=false,
                    IsWithoutErrorsCompleted=false
                },
                new EducationUsersProgress
                {
                    Id=6,
                    UserId=1,
                    EnglishLayoutLessonId=6,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=true,
                    IsSpeedCompleted=false,
                    IsWithoutErrorsCompleted=true
                },
                new EducationUsersProgress
                {
                    Id=7,
                    UserId=1,
                    EnglishLayoutLessonId=7,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=false,
                    IsSpeedCompleted=false,
                    IsWithoutErrorsCompleted=false
                },
                new EducationUsersProgress
                {
                    Id=8,
                    UserId=1,
                    EnglishLayoutLessonId=8,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=true,
                    IsSpeedCompleted=true,
                    IsWithoutErrorsCompleted=true
                },
                new EducationUsersProgress
                {
                    Id=9,
                    UserId=1,
                    EnglishLayoutLessonId=9,
                    EnglishLayoutLevelId=1,
                    IsLessThanTwoErrorsCompleted=false,
                    IsSpeedCompleted=false,
                    IsWithoutErrorsCompleted=false
                },
                new EducationUsersProgress
                {
                    Id=10,
                    UserId=1,
                    EnglishLayoutLessonId=10,
                    EnglishLayoutLevelId=2,
                    IsLessThanTwoErrorsCompleted=true,
                    IsSpeedCompleted=false,
                    IsWithoutErrorsCompleted=false
                },
                new EducationUsersProgress
                {
                    Id=11,
                    UserId=1,
                    EnglishLayoutLessonId=11,
                    EnglishLayoutLevelId=2,
                    IsLessThanTwoErrorsCompleted=true,
                    IsSpeedCompleted=false,
                    IsWithoutErrorsCompleted=false
                },

            };
        }
        
    }
}