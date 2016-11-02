using System;
using System.Collections.Generic;
using Moq;
using PresentationWebSite.Dal.Model;
using PresentationWebSite.Dal.UnitOfWorks.Base;
using PresentationWebSite.UI.WebMvc.Tests.Data;

namespace PresentationWebSite.UI.WebMvc.Tests.Controllers
{
    public class UnitOfWorkFakeFactory
    {
        private Mock<IBusinessUnitOfWork> _uow;

        public Mock<IBusinessUnitOfWork> Uow
        {
            get
            {
                if (_uow != null) return _uow;
                _uow = new Mock<IBusinessUnitOfWork>();
                _uow.Setup(u => u.LanguagesRepository).Returns(MockRepositoryFactory<Language>.Create(_languages).Object);
                _uow.Setup(u => u.TextsRepository).Returns(MockRepositoryFactory<Text>.Create(_texts).Object);
                _uow.Setup(u => u.GradesRepository).Returns(MockRepositoryFactory<Grade>.Create(Grades).Object);
                _uow.Setup(u => u.HobbiesRepository).Returns(MockRepositoryFactory<Hobby>.Create(_hobbies).Object);
                _uow.Setup(u => u.JobsRepository).Returns(MockRepositoryFactory<Job>.Create(_jobs).Object);
                _uow.Setup(u => u.SkillCategoriesRepository).Returns(MockRepositoryFactory<SkillCategory>.Create(_skillCategories).Object);
                _uow.Setup(u => u.SkillsRepository).Returns(MockRepositoryFactory<Skill>.Create(_skills).Object);
                _uow.Setup(u => u.UsersRepository).Returns(MockRepositoryFactory<ApplicationUser>.Create(_users).Object);
                _uow.Setup(u => u.WorksRepository).Returns(MockRepositoryFactory<Work>.Create(_works).Object);
                return _uow;
            }
        }

        private readonly List<Language> _languages = new List<Language>()
            {
                new Language() { Id = 1, CultureIsoCode = "fr-FR"},
                new Language() { Id = 2, CultureIsoCode = "en-US"}
            };


        private readonly List<Text> _texts = new List<Text>()
            {
                new Text() {Id = 1, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "DisplayWork1Fr"},
                new Text() {Id = 2, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "DisplayWork1En"},
                new Text() {Id = 3, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationTitleFr"},
                new Text() {Id = 4, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationTitleEn"},
                new Text() {Id = 5, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationSubTitleFr"},
                new Text() {Id = 6, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationSubTitleEn"},
                new Text() {Id = 7, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationFr"},
                new Text() {Id = 8, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationEn"},
                new Text() {Id = 9, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "grade1.fr"},
                new Text() {Id = 10, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "grade1.en"},
                new Text() {Id = 11, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "grade2.fr"},
                new Text() {Id = 12, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "grade2.en"},
                new Text() {Id = 14, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "skcat1.fr"},
                new Text() {Id = 15, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "skcat1.en"},
                new Text() {Id = 16, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk1.fr"},
                new Text() {Id = 17, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk1.en"},
                new Text() {Id = 18, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk2.fr"},
                new Text() {Id = 19, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk2.en"},
                new Text() {Id = 20, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "skcat2.fr"},
                new Text() {Id = 21, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "skcat2.en"},
                new Text() {Id = 22, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk3.fr"},
                new Text() {Id = 23, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk3.en"},
                new Text() {Id = 24, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk4.fr"},
                new Text() {Id = 25, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk4.en"},
                new Text() {Id = 26, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk1.fr"},
                new Text() {Id = 27, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk1.en"},
                new Text() {Id = 28, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk2.fr"},
                new Text() {Id = 29, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk2.en"},
                new Text() {Id = 30, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "job1.fr"},
                new Text() {Id = 31, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "job1.en"},
                new Text() {Id = 32, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk3.fr"},
                new Text() {Id = 33, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk3.en"},
                new Text() {Id = 34, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk4.fr"},
                new Text() {Id = 35, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk4.en"},
                new Text() {Id = 36, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "job2.fr"},
                new Text() {Id = 37, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "job2.en"},
                new Text() {Id = 38, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "hobby1.fr"},
                new Text() {Id = 39, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "hobby1.en"},
                new Text() {Id = 40, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "hobby2.fr"},
                new Text() {Id = 41, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "hobby2.en"}
            };

        private readonly List<ApplicationUser> _users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    Id = 1,
                    DisplayWork = new List<Text>()
                    {
                        new Text() {Id = 1, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "DisplayWork1Fr"},
                        new Text() {Id = 2, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "DisplayWork1En"}
                    },
                    PresentationTitleTexts = new List<Text>()
                    {
                        new Text() {Id = 3, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationTitleFr"},
                        new Text() {Id = 4, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationTitleEn"}
                    },
                    PresentationSubTitleTexts = new List<Text>()
                    {
                        new Text() {Id = 5, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationSubTitleFr"},
                        new Text() {Id = 6, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationSubTitleEn"}
                    },
                    ApplicationUserPresentations = new List<Text>()
                    {
                        new Text() {Id = 7, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "PresentationFr"},
                        new Text() {Id = 8, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "PresentationEn"}
                    },
                    City = "MyCity",
                    LinkedInUrl = "Htpp://linkedInUrl.com/",
                    FamilyName = "MyFamilyName",
                    TwitterName = "MyTwitterName",
                    DateOfBirth = new DateTime(2000, 1, 15),
                    FirstName = "MyFirstName",
                    PhoneNumber = "0600000000",
                    ZipCode = "66000",
                    Email = "me@email.com"
                }
            };

        private List<Grade> _grd;
        private List<Grade> Grades => _grd ?? (_grd = new List<Grade>()
        {
            new Grade()
            {
                Id = 1,
                ObtainingDateTime = new DateTime(2010,1,15),
                Texts = new List<Text>()
                        {
                            new Text() {Id = 9, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "grade1.fr"},
                            new Text() {Id = 10, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "grade1.en"}
                        }
            },
            new Grade()
            {
                Id = 2,
                ObtainingDateTime = new DateTime(2010,1,15),
                Texts = new List<Text>()
                        {
                            new Text() {Id = 11, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "grade2.fr"},
                            new Text() {Id = 12, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "grade2.en"}
                        }
            }
        });

        private List<SkillCategory> _skillCategories = new List<SkillCategory>()
        {
            new SkillCategory()
            {
                Id = 1,
                DisplayPriority = 0,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 14, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "skcat1.fr"},
                            new Text() {Id = 15, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "skcat1.en"}
                        },
                Skills = new List<Skill>()
                        {
                            new Skill()
                            {
                                Id = 1,
                                KnowledgePercent = 50,
                                Texts = new List<Text>()
                                        {
                                            new Text() {Id = 16, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk1.fr"},
                                            new Text() {Id = 17, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk1.en"}
                                        }
                            },
                            new Skill()
                            {
                                Id = 2,
                                KnowledgePercent = 80,
                                Texts = new List<Text>()
                                        {
                                            new Text() {Id = 18, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk2.fr"},
                                            new Text() {Id = 19, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk2.en"}
                                        }
                            }
                        }
            },
            new SkillCategory()
            {
                Id = 2,
                DisplayPriority = 5,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 20, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "skcat2.fr"},
                            new Text() {Id = 21, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "skcat2.en"}
                        },
                Skills = new List<Skill>()
                        {
                            new Skill()
                            {
                                Id = 3,
                                KnowledgePercent = 30,
                                Texts = new List<Text>()
                                        {
                                            new Text() {Id = 22, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk3.fr"},
                                            new Text() {Id = 23, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk3.en"}
                                        }
                            },
                            new Skill()
                            {
                                Id = 4,
                                KnowledgePercent = 90,
                                Texts = new List<Text>()
                                        {
                                            new Text() {Id = 24, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk4.fr"},
                                            new Text() {Id = 25, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk4.en"}
                                        }
                            }
                        }
            }
        };
        private List<Skill> _skills = new List<Skill>()
        {
            new Skill()
            {
                Id = 1,
                KnowledgePercent = 50,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 16, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk1.fr"},
                            new Text() {Id = 17, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk1.en"}
                        }
            },
            new Skill()
            {
                Id = 2,
                KnowledgePercent = 80,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 18, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk2.fr"},
                            new Text() {Id = 19, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk2.en"}
                        }
            },
            new Skill()
            {
                Id = 3,
                KnowledgePercent = 30,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 22, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk3.fr"},
                            new Text() {Id = 23, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk3.en"}
                        }
            },
            new Skill()
            {
                Id = 4,
                KnowledgePercent = 90,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 24, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "sk4.fr"},
                            new Text() {Id = 25, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "sk4.en"}
                        }
            }
        };

        private List<Work> _works = new List<Work>()
        {
            new Work()
            {
                Id = 1,
                DisplayPriority = 1,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 26, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk1.fr"},
                            new Text() {Id = 27, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk1.en"}
                        }
            },
            new Work()
            {
                Id = 2,
                DisplayPriority = 2,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 28, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk2.fr"},
                            new Text() {Id = 29, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk2.en"}
                        }
            },
            new Work()
            {
                Id = 3,
                DisplayPriority = 1,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 32, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk3.fr"},
                            new Text() {Id = 33, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk3.en"}
                        }
            },
            new Work()
            {
                Id = 4,
                DisplayPriority = 2,
                Texts = new List<Text>()
                        {
                            new Text() {Id = 34, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk4.fr"},
                            new Text() {Id = 35, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk4.en"}
                        }
            }
        };

        private List<Job> _jobs = new List<Job>()
        {
            new Job()
            {
                Id = 1,
                StarterDate = new DateTime(2000,1,15),
                EndDate = new DateTime(2001,1,15),
                Texts = new List<Text>()
                        {
                            new Text() {Id = 30, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "job1.fr"},
                            new Text() {Id = 31, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "job1.en"}
                        },
                Works = new List<Work>()
                {
                    new Work()
                    {
                        Id = 1,
                        DisplayPriority = 1,
                        Texts = new List<Text>()
                                {
                                    new Text() {Id = 26, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk1.fr"},
                                    new Text() {Id = 27, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk1.en"}
                                }
                    },
                    new Work()
                    {
                        Id = 2,
                        DisplayPriority = 2,
                        Texts = new List<Text>()
                                {
                                    new Text() {Id = 28, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk2.fr"},
                                    new Text() {Id = 29, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk2.en"}
                                }
                    }
                }
            },
            new Job()
            {
                Id = 2,
                StarterDate = new DateTime(2000,1,15),
                EndDate = new DateTime(2001,1,15),
                Texts = new List<Text>()
                        {
                            new Text() {Id = 36, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "job2.fr"},
                            new Text() {Id = 37, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "job2.en"}
                        },
                Works = new List<Work>()
                {
                    new Work()
                    {
                        Id = 3,
                        DisplayPriority = 1,
                        Texts = new List<Text>()
                                {
                                    new Text() {Id = 26, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk3.fr"},
                                    new Text() {Id = 27, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk3.en"}
                                }
                    },
                    new Work()
                    {
                        Id = 4,
                        DisplayPriority = 2,
                        Texts = new List<Text>()
                                {
                                    new Text() {Id = 28, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "wk4.fr"},
                                    new Text() {Id = 29, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "wk4.en"}
                                }
                    }
                }
            },
        };

        private List<Hobby> _hobbies = new List<Hobby>()
        {
            new Hobby()
            {
                Id = 1,
                Texts = new List<Text>()
                {
                    new Text() {Id = 38, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "hobby1.fr"},
                    new Text() {Id = 39, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "hobby1.en"}
                },
                Content = new byte[50]
            },
            new Hobby()
            {
                Id = 2,
                Texts = new List<Text>()
                {
                    new Text() {Id = 40, Language = new Language() { Id = 1, CultureIsoCode = "fr-FR"}, Value = "hobby2.fr"},
                    new Text() {Id = 41, Language = new Language() { Id = 2, CultureIsoCode = "en-US"}, Value = "hobby2.en"}
                },
                Content = new byte[150]
            },
        };
        

    }
}
