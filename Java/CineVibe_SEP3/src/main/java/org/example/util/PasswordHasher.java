package org.example.util;

import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;

public class PasswordHasher {

    private static final BCryptPasswordEncoder encoder = new BCryptPasswordEncoder();

    /**
     * Hashes a plain text password using BCrypt.
     *
     * @param plainPassword The plain text password.
     * @return The hashed password.
     */
    public static String hashPassword(String plainPassword) {
        return encoder.encode(plainPassword);
    }

    public static void main(String[] args) {
        // Customer passwords
        System.out.println("Customer Password Hashes:");
        System.out.println("john_doe: " + hashPassword("hashed_password_1"));
        System.out.println("jane_smith: " + hashPassword("hashed_password_2"));
        System.out.println("alice_brown: " + hashPassword("hashed_password_3"));
        System.out.println("charlie_jones: " + hashPassword("hashed_password_4"));
        System.out.println("daniel_white: " + hashPassword("hashed_password_5"));
        System.out.println("emily_davis: " + hashPassword("hashed_password_6"));
        System.out.println("frank_miller: " + hashPassword("hashed_password_7"));
        System.out.println("grace_wilson: " + hashPassword("hashed_password_8"));
        System.out.println("henry_moore: " + hashPassword("hashed_password_9"));
        System.out.println("isabella_taylor: " + hashPassword("hashed_password_10"));
        System.out.println("jack_lee: " + hashPassword("hashed_password_11"));
        System.out.println("karen_clark: " + hashPassword("hashed_password_12"));
        System.out.println("liam_hall: " + hashPassword("hashed_password_13"));
        System.out.println("mia_scott: " + hashPassword("hashed_password_14"));
        System.out.println("nathan_walker: " + hashPassword("hashed_password_15"));
        System.out.println("olivia_harris: " + hashPassword("hashed_password_16"));
        System.out.println("paul_robinson: " + hashPassword("hashed_password_17"));
        System.out.println("quinn_cooper: " + hashPassword("hashed_password_18"));
        System.out.println("ruby_wood: " + hashPassword("hashed_password_19"));
        System.out.println("samuel_kelly: " + hashPassword("hashed_password_20"));

        // Admin passwords
        System.out.println("\nAdmin Password Hashes:");
        System.out.println("admin_user: " + hashPassword("admin_password"));
        System.out.println("super_admin: " + hashPassword("super_password"));
    }
}
