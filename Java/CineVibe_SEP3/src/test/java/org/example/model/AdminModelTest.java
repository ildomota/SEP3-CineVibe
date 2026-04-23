package org.example.model;

import org.junit.jupiter.api.Test;

import java.time.LocalDateTime;

import static org.junit.jupiter.api.Assertions.*;

public class AdminModelTest {

    @Test
    public void testAdminModel() {
        LocalDateTime now = LocalDateTime.now();
        Admin admin = new Admin("admin_user", "admin_password", "admin@example.com", "Active");


        assertEquals("admin_user", admin.getUsername());
        assertEquals("admin_password", admin.getPassword_hash());
        assertEquals("admin@example.com", admin.getEmail());
        assertEquals("Active", admin.getStatus());

    }
}
