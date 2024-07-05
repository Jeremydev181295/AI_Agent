```java
import java.util.regex.*;
import javax.mail.internet.AddressException;
import javax.mail.internet.InternetAddress;

public class User {
    private String name;
    private String email;
    private String password;
    private String profilePicture;

    public User(String name, String email, String password, String profilePicture) {
        this.name = name;
        this.email = email;
        this.password = password;
        this.profilePicture = profilePicture;
    }

    public boolean isEmailValid() {
        try {
            InternetAddress emailAddress = new InternetAddress(email);
            emailAddress.validate();
        } catch (AddressException ex) {
            return false;
        }
        return true;
    }

    public boolean isPasswordValid() {
        String passwordPattern = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\\S+$).{8,}$";
        Pattern pattern = Pattern.compile(passwordPattern);
        Matcher matcher = pattern.matcher(password);
        return matcher.matches();
    }

    public boolean register() {
        if (!isEmailValid()) {
            System.out.println("Error: Invalid email address");
            return false;
        }
        if (!isPasswordValid()) {
            System.out.println("Error: Password does not meet the criteria");
            return false;
        }
        // Here you would add the code to save the user data into your database
        // and send the email with the verification link
        // For now we will just simulate these actions with a print statement
        System.out.println("User registered successfully. A verification link has been sent to your email address");
        return true;
    }

    public void activateAccount() {
        // Here you would add the code to activate the user account in your database
        // For now we will just simulate this action with a print statement
        System.out.println("User account activated successfully. Welcome " + name);
    }
}
```