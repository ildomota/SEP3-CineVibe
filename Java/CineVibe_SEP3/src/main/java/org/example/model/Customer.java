package org.example.model;

import jakarta.persistence.*;
import java.time.LocalDateTime;

@Entity
@Table(name = "customer", schema = "cinevibe")
public class Customer {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "customer_id")
    private Integer customer_id;

    @Column(name = "username", nullable = false, unique = true)
    private String username;

    @Column(name = "password_hash", nullable = false)
    private String password_hash;

    @Column(name = "email", unique = true)
    private String email;

    @Column(name = "status", nullable = false)
    private String status = "Active";

    @Column(name = "loyaltypoints", nullable = false)
    private Integer loyaltyPoints = 0;

    @Column(name = "created_at", nullable = false, updatable = false)
    private LocalDateTime created_at = LocalDateTime.now();

    @Column(name = "updated_at", nullable = false)
    private LocalDateTime updated_at = LocalDateTime.now();

    public Customer() {}

    public Customer(String username, String password_hash, String email, String status, Integer loyaltyPoints) {
        this.username = username;
        this.password_hash = password_hash;
        this.email = email;
        this.status = status;
        this.loyaltyPoints = loyaltyPoints;

    }

    public Integer getCustomerId() {
        return customer_id;
    }

    public void setCustomerId(Integer customer_id) {
        this.customer_id = customer_id;
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

    public Integer getLoyaltyPoints() {
        return loyaltyPoints;
    }

    public void setLoyaltyPoints(Integer loyaltyPoints) {
        this.loyaltyPoints = loyaltyPoints;
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
}
