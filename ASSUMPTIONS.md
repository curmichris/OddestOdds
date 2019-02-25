Assumptions


## List 
* A punter has to view all 1 x 2 odds in one screen.
* Odds can be viewed by an Administrator in one screen.
* Odds can be added by an Administrator.
* Odds can be edited by an Administrator.
* Odds can be deleted by an Administrator.
* Odds for all users accessing the site need to be updated in real-time 

## Detailed
In context of this assignment, I have assumed that by real-time updates, the author of the test demanded that if an odd is Added, 
edited or deleted by the Administrator, then it is required that the punter sees these new odds without the need to refresh or reload the website. 
I did this assumption considering the crucial feature the system needs to have, to provide its punters with updated and correct odds in real-time.
To make this process as seamless as possible, I have decided to include Microsoft’s SingalR Core library. 
This library made the process of adding real-time web updates and functionalities to the application. The library is being called whenever the 
‘Populate Fields’ button in the Admin area is clicked, thus firing an event and refreshes the whole list of odds, for all users currently
accessing the application Punters are now able to view the updated odds in real-time and can determine the new/updated odds without having to refresh the web-page. 

