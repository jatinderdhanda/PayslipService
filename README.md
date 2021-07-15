# PayslipService

This project contain one GET and one POST mehtod.

GET Method:

This require payslip id as paramter to be passed. As here we dont use actual external database but in memory database, we have added some entries there.
567098, 567090, 567998, 567089 would give the success response. Any other entries will give failure response.
No admin claims required for GET.

POST Method:

This method allows the user to add new employees. Again because there is no actual external database, it will be stoed in memory and will return success response.
This require admin claims, so use "admin" for both username and password.
Some basic validation (cannot be empty) is implemented using fluent Validation.

