package org.example.service;

import org.example.model.Admin;
import org.example.repository.AdminRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class AdminService {

    @Autowired
    private AdminRepository adminRepository;

    private final BCryptPasswordEncoder passwordEncoder = new BCryptPasswordEncoder();

    // Retrieve all admins
    public List<Admin> getAllAdmins() {
        return adminRepository.findAll();
    }

    // Retrieve admin by ID
    public Admin getAdminById(Integer id) {
        return adminRepository.findById(id).orElseThrow(() -> new RuntimeException("Admin not found"));
    }

    // Create a new admin with hashed password
    public Admin createAdmin(Admin admin) {
        // Hash the password before saving it
        admin.setPasswordHash(passwordEncoder.encode(admin.getPasswordHash()));
        admin.setStatus("Admin"); // Ensure the status is set to Admin
        return adminRepository.save(admin);
    }

    // Update an admin
    public Admin updateAdmin(Integer id, Admin updatedAdmin) {
        Admin existingAdmin = getAdminById(id);
        existingAdmin.setUsername(updatedAdmin.getUsername());
        existingAdmin.setPasswordHash(passwordEncoder.encode(updatedAdmin.getPasswordHash())); // Hash new password
        existingAdmin.setEmail(updatedAdmin.getEmail());
        existingAdmin.setStatus(updatedAdmin.getStatus());
        existingAdmin.setUpdatedAt(LocalDateTime.now());
        return adminRepository.save(existingAdmin);
    }

    // Delete an admin
    public void deleteAdmin(Integer id) {
        if (adminRepository.existsById(id)) {
            adminRepository.deleteById(id);
        } else {
            throw new RuntimeException("Admin not found");
        }
    }

    // Admin login
    public Admin login(String username, String password, String secretCode) {
        if (!"010108".equals(secretCode)) {
            throw new RuntimeException("Invalid secret code");
        }
        Admin admin = adminRepository.findByUsername(username);
        if (admin == null || !passwordEncoder.matches(password, admin.getPasswordHash())) {
            throw new RuntimeException("Invalid username or password");
        }
        return admin; // Login successful
    }
}
