Feature: FeaturesLink

Scenario: Verify the feature page title
Given Navigate to Lime Home Page
When Click on feature page link
Then "Features" feature title should be displayed

Scenario: Verify the redirection of reload feature button
Given Navigate to Lime Home Page
When Click on feature page link
And Click on reload feature button
Then "Caminho.LEME" title should be displayed

