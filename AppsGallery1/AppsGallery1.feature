Feature: AppsGallery1
	

@mytag
Scenario:  list applications
   Given the alteryx is running at "http://gallery.alteryx.com"
   When I Invoke the GET at "api/apps/gallery"
   Then I see at least 20 active apps
