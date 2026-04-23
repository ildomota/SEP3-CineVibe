package org.example.model;

import jakarta.persistence.*;
import java.time.LocalDateTime;

@Entity
@Table(name = "admin", schema = "cinevibe")
public class Admin {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "admin_id")
    private Integer admin_id;

    @Column(name = "username", nullable = false, unique = true)
    private String username;

    @Column(name = "password_hash", nullable = false)
    private String password_hash;

    @Column(name = "email", unique = true)
    private String email;

    @Column(name = "status", nullable = false)
    private String status = "Admin";

    @Column(name = "created_at", nullable = false, updatable = false)
    private LocalDateTime created_at = LocalDateTime.now();

    @Column(name = "updated_at", nullable = false)
    private LocalDateTime updated_at = LocalDateTime.now();

    @Transient
    private String secretCode;

    // Default Constructor
    public Admin() {}

    // Parameterized Constructor
    public Admin(String username, String password_hash, String email, String status, String secretCode) {
        this.username = username;
        this.password_hash = password_hash;
        this.email = email;
        this.status = status;
        this.secretCode = secretCode;
    }

    // Getters and Setters
    public Integer getAdminId() {
        return admin_id;
    }

    public void setAdminId(Integer admin_id) {
        this.admin_id = admin_id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPasswordHash() {
        return password_hash;
    }

    public void setPasswordHash(String password_hash) {
        this.password_hash = password_hash;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public LocalDateTime getCreatedAt() {
        return created_at;
    }

    public void setCreatedAt(LocalDateTime created_at) {
        this.created_at = created_at;
    }

    public LocalDateTime getUpdatedAt() {
        return updated_at;
    }

    public void setUpdatedAt(LocalDateTime updated_at) {
        this.updated_at = updated_at;
    }

    public String getSecretCode() {
        return secretCode;
    }

    public void setSecretCode(String secretCode) {
        this.secretCode = secretCode;
    }
}
