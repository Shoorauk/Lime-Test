Feature: CAM-8 page scnarios

Scenario: Add a Material Type and verify the Material Type in Search page
Given Navigate to Lime Home Page
When the user clicked on CAM-8
Then "Search & Manage" page should be displayed
When the user clicks on Add button 
And enter the <MaterialType> and <Description> 
And Click on "Save"
Then the new Material Type is listed in the Search & Manage Page
Examples: 
| MaterialType | Description        |
| Auto Test120   | Material type test 1000|


Scenario: Verify the home page title
Given Navigate to Lime Home Page
When Click on home page link
Then "Caminho.LIME" title should be displayed

Scenario: Validate the material type text
Given Navigate to Lime Home Page
When the user clicked on CAM-8
Then "Material Types" Material type text should be displayed