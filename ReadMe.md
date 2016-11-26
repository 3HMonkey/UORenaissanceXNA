**UltimaXNA: Open Source Ultima Online Client in C#/XNA**

[![Join the chat at https://gitter.im/ZaneDubya/UltimaXNA](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/ZaneDubya/UltimaXNA?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

UltimaXNA is an isometric massively multiplayer online role playing client that supports playing Ultima Online on your favorite freeshards! The client is already playable, but not all features are implemented. We are currently working on [Milestone 0.8](https://github.com/ZaneDubya/UltimaXNA/milestones), which focuses on bugfixes and adding features not used in daily play.

![Ultima Online at 1920p60](https://cloud.githubusercontent.com/assets/7041719/9290206/9215ffac-434f-11e5-8739-0739567008d1.jpg)
[Watch a video of UltimaXNA Alpha at Milestone 0.4 (April 3, 2015)](http://www.youtube.com/watch?v=gUfpQkLBdzE)

**Getting started**

You should use [Visual Studio 2015](https://www.visualstudio.com/vs/community/) to develop and compile UltimaXNA. You will also need to install [XNA for Visual Studio 2015](https://blogs.msdn.microsoft.com/uk_faculty_connection/2015/11/02/installing-xna-with-visual-studio-2015/).

UltimaXNA does not include the Ultima Online resource files, and will not work on a computer that does not have Ultima Online installed. UltimaXNA is compatible with any Ultima Online client prior to the introduction of the UOP resource format, including Mondain's Legacy. [Download the client files for Mondain's Legacy here](http://download.cnet.com/Ultima-Online-Mondain-s-Legacy-client/3000-7540_4-10432237.html).

UltimaXNA is designed to work with all server software, but we test exclusively on RunUO 2.6, [which you can download here](https://github.com/runuo/runuo/releases). Note that you will [need to change three lines of the RunUO server scripts](https://github.com/ZaneDubya/UltimaXNA/wiki) to allow it to work with Mondain's Legacy resource files.
