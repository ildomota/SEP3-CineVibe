package org.example.service;

import org.example.model.Admin;
import org.example.repository.AdminRepository;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

@SpringBootTest
class AdminServiceTest {

    @Autowired
    private AdminService adminService;

    @Autowired
    private AdminRepository adminRepository;

    @Test
    void testGetAllAdmins() {
        adminRepository.save(new Admin("admin_user3", "admin_password", "admin3@example.com", "Active"));


        List<Admin> admins = adminService.getAllAdmins();
        assertEquals(3, admins.size());
        assertEquals("admin_user", admins.get(0).getUsername());
    }
    @Test
    void testLoginSuccess() {
        // Verify login with correct credentials
        Admin admin = adminService.login("admin_user", "admin_password");
        assertNotNull(admin);
        assertEquals("admin_user", admin.getUsername());
    }

    @Test
    void testLoginInvalidPassword() {
        // Verify login with wrong password
        RuntimeException exception = assertThrows(RuntimeException.class, () -> {
            adminService.login("admin_user", "wrong_password");
        });
        assertEquals("Invalid username or password", exception.getMessage());
    }

    @Test
    void testLoginUserNotFound() {
        // Verify login with non-existent user
        RuntimeException exception = assertThrows(RuntimeException.class, () -> {
            adminService.login("non_existent_user", "admin_password");
        });
        assertEquals("Invalid username or password", exception.getMessage());
    }
}
