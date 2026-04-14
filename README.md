# Applied Cryptography Labs

C# cryptography lab projects covering steganography, hashing, encryption, and public-key concepts.

## Overview

This repository contains C#/.NET cryptography projects focused on steganography, hashing, encryption, Diffie-Hellman, and RSA.

The projects are small console programs centered on specific cryptographic concepts rather than a single unified application.

## Contents

- `P1/P1_1/`: bitmap steganography project working directly with image byte data
- `P1/P1_2/`: cryptanalysis exercise based on seeded encryption and random number weakness
- `P2/`: salted MD5 collision search using a birthday-attack approach
- `P3/`: Diffie-Hellman key derivation with AES encryption and decryption
- `P4/`: RSA project using the Extended Euclidean algorithm for encryption and decryption

## How to Run

1. Open the specific project folder you want to inspect, such as `P2` or `P4`.
2. Restore dependencies if needed with `dotnet restore`.
3. Run the project with `dotnet run -- <arguments>` using the input format expected by that project.

## Context

CSE 539 - Applied Cryptography (ASU)

This repository contains applied cryptography lab work from an ASU course. It keeps the C# source projects while excluding build artifacts, submission archives, and course PDF materials.
