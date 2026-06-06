using System;
using System.Collections.Generic;

// Comment class - responsible for tracking the name of the person who made the comment and the text of the comment
class Comment
{
    private string commenterName;
    private string commentText;

    public Comment(string commenterName, string commentText)
    {
        this.commenterName = commenterName;
        this.commentText = commentText;
    }

    public string GetDisplayText()
    {
        return $"{commenterName}: \"{commentText}\"";
    }
}

// Video class - responsible for tracking title, author, length, and list of comments
class Video
{
    private string title;
    private string author;
    private int lengthInSeconds;
    private List<Comment> comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        this.title = title;
        this.author = author;
        this.lengthInSeconds = lengthInSeconds;
        this.comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return comments.Count;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Author: {author}");
        Console.WriteLine($"Length: {lengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");

        foreach (Comment comment in comments)
        {
            Console.WriteLine($"  - {comment.GetDisplayText()}");
        }
        Console.WriteLine();
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        // Create a list to store all videos
        List<Video> videos = new List<Video>();

        // Create Video 1
        Video video1 = new Video("Understanding Abstraction in OOP", "TechWithMesh", 450);
        video1.AddComment(new Comment("MichealAmadi", "This really helped me understand abstraction!"));
        video1.AddComment(new Comment("OnyeakachiAmadi", "Great examples, very clear explanation"));
        video1.AddComment(new Comment("MeshHub", "Finally it clicks! Thanks for this video"));
        videos.Add(video1);

        // Create Video 2
        Video video2 = new Video("Top 10 Programming Tips 2024", "CodeWithMosh", 720);
        video2.AddComment(new Comment("DevLife", "Number 7 changed my workflow completely"));
        video2.AddComment(new Comment("JaneDoe", "Can you make a part 2 with more advanced tips?"));
        video2.AddComment(new Comment("PythonLover", "These tips apply to all languages, not just JavaScript"));
        video2.AddComment(new Comment("QuickLearner", "Saved this to my favorites, very useful!"));
        videos.Add(video2);

        // Create Video 3
        Video video3 = new Video("Building Your First Game in Unity", "Brackeys", 1200);
        video3.AddComment(new Comment("GameDevBeginner", "Followed along and made my first game!"));
        video3.AddComment(new Comment("UnityPro", "Good foundation tutorial for newcomers"));
        video3.AddComment(new Comment("IndieDev", "Would love to see a follow-up on optimization"));
        videos.Add(video3);

        // Create Video 4
        Video video4 = new Video("Machine Learning Basics Explained", "TwoMinutePapers", 360);
        video4.AddComment(new Comment("DataScientist", "Concise and informative as always"));
        video4.AddComment(new Comment("MLStudent", "This channel helped me pass my AI course"));
        video4.AddComment(new Comment("TechEnthusiast", "Amazing how complex topics can be simplified"));
        video4.AddComment(new Comment("FutureDeveloper", "Subscribed after watching this!"));
        videos.Add(video4);

        // Iterate through the list and display each video's information
        foreach (Video video in videos)
        {
            video.DisplayInfo();
        }
    }
}