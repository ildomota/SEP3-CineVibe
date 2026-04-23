package org.example.repository;

import jakarta.transaction.Transactional;
import org.example.model.Admin;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.time.LocalDateTime;
import java.util.List;

import static org.junit.jupiter.api.Assertions.*;

@SpringBootTest
@Transactional
public class AdminRepositoryTest {

    @Autowired
    private AdminRepository adminRepository;

    @Test
    public void testFindByUsername() {
        Admin admin = adminRepository.findByUsername("admin_user");
        assertNotNull(admin);
        assertEquals("admin@example.com", admin.getEmail());
    }

    @Test
    @Transactional
    public void testSaveAndFindAll() {
        Admin newAdmin = new Admin();
        newAdmin.setUsername("new_admin");
        newAdmin.setPasswordHash("new_password");
        newAdmin.setEmail("new_admin@example.com");
        newAdmin.setStatus("Active");
        newAdmin.setCreatedAt(LocalDateTime.now());
        newAdmin.setUpdatedAt(LocalDateTime.now());

        // Save the new admin
        adminRepository.save(newAdmin);

        // Retrieve all admins
        List<Admin> admins = adminRepository.findAll();

        assertNotNull(admins);
        assertFalse(admins.isEmpty());
        assertEquals(4, admins.size()); // Assuming 2 admins already exist in the mock data
    }

}
