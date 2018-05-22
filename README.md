# North Shore Health Network Group Project

## Description

The North Shore Health Network is currently a group of health centers that have merged to form the North Shore Health Network to better serve the community of the North Shore area. As a group project, we were tasked to create a mock redesign of their current (http://www.nshn.care/, website). Our process began with applying a content inventory, followed by the personification of the potential visitors to the hospital, then a number of card sorts, followed by various responsive wireframes, a mockup, and a theme template. Over the course of 4 months, we were able to deploy our website, found (https://nshn-hospital.azurewebsites.net/, here). 

Guided by their belief of community and nature, we redesigned their website to reflect that by utilizing colors found primarily outdoors (shades of blues and greens).

## Functionality

Our redesign has 3 layers of basic access. First, there are anonymous users who have basic access to reading content on the website. Second, users can register to become a member of the NSHN hospital, and have special access to pages and functions such as looking up their lab results, booking appointments, and adding comments to news articles. Finally, there is administrator access. Those who are granted admin access are able to perform a wide range of things, such as deleting news articles, uploading new lab results, and viewing a list of all donors/donations. 

The way we've created our tables, allows us to add/remove user roles as need be, and as such we can/have implemented other user roles such as doctors and nurses who are the only ones allowed to update lab results. This allows us to future proof and further segregate access in the future.

We have also implemented security measures to prevent users from manipulating the URL, and added 404/restricted access pages. Each of our features have full CRUD (Create, Read, Update and Delete) functionality, but access is again restricted to different users with different roles.

## Technologies Used

Type | Names
--- | ---
Programming Languages | * C# * JavaScript * HTML5 * CSS 
Frameworks | * Bootstrap * MVC * jQuery
Database | * MSSQL Azure
IDEs | * Atom * Visual Code * Sublime


## Contributors

Our group consisted of 5 individuals all coming from a variety of different backgrounds, ranging from designers, to electrical engineers, to scientists. In just a span of 8 months, we were able to collectively put together 10 features. The creators and their associated pages are depicted below:

### Yeganeh Ghasemi
@hazeoscar

* Hospital Sites
* FAQ
* Departments

### Elizabeth Reji
@emreji
* Book Appointments
* Lab Results

### Joseph Chow
@josphchw

* Volunteer
* Tours

### Karan Dhillon
@karandhillon1995

* Feedback
* Contact Us

### Jessica Wong
@jwong92

* Donations
* News


