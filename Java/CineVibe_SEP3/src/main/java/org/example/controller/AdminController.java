package org.example.controller;

import org.example.model.Admin;
import org.example.model.LoginRequest;
import org.example.repository.AdminRepository;
import org.example.service.AdminService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/admins")
public class AdminController {

    @Autowired
    private AdminService adminService;

    // Retrieve all admins
    @GetMapping
    public ResponseEntity<List<Admin>> getAllAdmins() {
        return ResponseEntity.ok(adminService.getAllAdmins());
    }

    // Create a new admin
    @PostMapping("/register")
    public ResponseEntity<?> registerAdmin(@RequestBody Admin admin) {
        try {
            if (!"010108".equals(admin.getSecretCode())) {
                return ResponseEntity.status(HttpStatus.FORBIDDEN).body("Invalid secret code.");
            }
            Admin createdAdmin = adminService.createAdmin(admin);
            return ResponseEntity.ok(createdAdmin);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR).body("Error: " + e.getMessage());
        }
    }

    // Retrieve admin by ID
    @GetMapping("/{id}")
    public ResponseEntity<Admin> getAdminById(@PathVariable Integer id) {
        Admin admin = adminService.getAdminById(id);
        return ResponseEntity.ok(admin);
    }

    // Update admin details
    @PutMapping("/{id}")
    public ResponseEntity<Admin> updateAdmin(@PathVariable Integer id, @RequestBody Admin updatedAdmin) {
        Admin updated = adminService.updateAdmin(id, updatedAdmin);
        return ResponseEntity.ok(updated);
    }

    // Delete an admin
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteAdmin(@PathVariable Integer id) {
        adminService.deleteAdmin(id);
        return ResponseEntity.noContent().build();
    }

    // Admin login
    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody LoginRequest loginRequest) {
        try {
            Admin admin = adminService.login(loginRequest.getUsername(), loginRequest.getPassword(), loginRequest.getSecretCode());
            return ResponseEntity.ok(admin);
        } catch (RuntimeException ex) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(ex.getMessage());
        }
    }
}
