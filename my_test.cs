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

    public boolean isValidEmail(String email) {
        boolean result = true;
        try {
            InternetAddress emailAddr = new InternetAddress(email);
            emailAddr.validate();
        } catch (AddressException ex) {
            result = false;
        }
        return result;
    }

    public boolean isValidPassword(String password) {
        Pattern pattern;
        Matcher matcher;
        String PASSWORD_PATTERN = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\\S+$).{8,}$";
        pattern = Pattern.compile(PASSWORD_PATTERN);
        matcher = pattern.matcher(password);
        return matcher.matches();
    }

    public void registerUser() {
        if (!isValidEmail(this.email)) {
            System.out.println("Invalid email address.");
            return;
        }

        if (!isValidPassword(this.password)) {
            System.out.println("Password must contain at least 8 characters, including at least one uppercase letter, one lowercase letter, one number, and one special character.");
            return;
        }

        // Code to store user data and send confirmation email goes here
    }
}
```