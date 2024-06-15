namespace CourseProjectKeyboardApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.educationUsersProgresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        is_less_than_two_errors_completed = c.Boolean(nullable: false),
                        is_without_errors_completed = c.Boolean(nullable: false),
                        id_speed_completed = c.Boolean(nullable: false),
                        user_id = c.Int(nullable: false),
                        english_layout_level_id = c.Int(nullable: false),
                        english_layout_lesson_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.englishLayoutLessons", t => t.english_layout_lesson_id)
                .ForeignKey("dbo.englishLayoutLevels", t => t.english_layout_level_id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.user_id)
                .Index(t => t.english_layout_level_id)
                .Index(t => t.english_layout_lesson_id);
            
            CreateTable(
                "dbo.englishLayoutLessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        text = c.String(nullable: false, maxLength: 128),
                        ordinal = c.Int(nullable: false),
                        english_layout_level_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.englishLayoutLevels", t => t.english_layout_level_id)
                .Index(t => t.english_layout_level_id);
            
            CreateTable(
                "dbo.englishLayoutLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 32),
                        ordinal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 32),
                        login = c.String(nullable: false, maxLength: 16),
                        email = c.String(nullable: false, maxLength: 42),
                        password = c.String(nullable: false),
                        avatar_path = c.String(),
                        english_layout_level_id = c.Int(nullable: false),
                        english_layout_lesson_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.englishLayoutLessons", t => t.english_layout_lesson_id)
                .ForeignKey("dbo.englishLayoutLevels", t => t.english_layout_level_id)
                .Index(t => t.english_layout_level_id)
                .Index(t => t.english_layout_lesson_id);
            
            CreateTable(
                "dbo.typingTestResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        speed = c.Int(nullable: false),
                        accuracy_percent = c.Double(nullable: false),
                        layout_type = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.typingTestResults", "user_id", "dbo.users");
            DropForeignKey("dbo.educationUsersProgresses", "user_id", "dbo.users");
            DropForeignKey("dbo.users", "english_layout_level_id", "dbo.englishLayoutLevels");
            DropForeignKey("dbo.users", "english_layout_lesson_id", "dbo.englishLayoutLessons");
            DropForeignKey("dbo.educationUsersProgresses", "english_layout_level_id", "dbo.englishLayoutLevels");
            DropForeignKey("dbo.educationUsersProgresses", "english_layout_lesson_id", "dbo.englishLayoutLessons");
            DropForeignKey("dbo.englishLayoutLessons", "english_layout_level_id", "dbo.englishLayoutLevels");
            DropIndex("dbo.typingTestResults", new[] { "user_id" });
            DropIndex("dbo.users", new[] { "english_layout_lesson_id" });
            DropIndex("dbo.users", new[] { "english_layout_level_id" });
            DropIndex("dbo.englishLayoutLessons", new[] { "english_layout_level_id" });
            DropIndex("dbo.educationUsersProgresses", new[] { "english_layout_lesson_id" });
            DropIndex("dbo.educationUsersProgresses", new[] { "english_layout_level_id" });
            DropIndex("dbo.educationUsersProgresses", new[] { "user_id" });
            DropTable("dbo.typingTestResults");
            DropTable("dbo.users");
            DropTable("dbo.englishLayoutLevels");
            DropTable("dbo.englishLayoutLessons");
            DropTable("dbo.educationUsersProgresses");
        }
    }
}
