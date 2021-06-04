﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("API.Data.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("dateRead")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("messageSent")
                        .HasColumnType("TEXT");

                    b.Property<bool>("recipientDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("recipientId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("senderDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("senderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("recipientId");

                    b.HasIndex("senderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("city")
                        .HasColumnType("TEXT");

                    b.Property<string>("country")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("dateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("interests")
                        .HasColumnType("TEXT");

                    b.Property<string>("introduction")
                        .HasColumnType("TEXT");

                    b.Property<string>("knownAs")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("lastActive")
                        .HasColumnType("TEXT");

                    b.Property<string>("lookingFor")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("passWordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("passWordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("userName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("appUserId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("isMain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("publicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("url")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("appUserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Entities.UserLike", b =>
                {
                    b.Property<int>("sourceUserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("likedUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("sourceUserId", "likedUserId");

                    b.HasIndex("likedUserId");

                    b.ToTable("UserLikes");
                });

            modelBuilder.Entity("API.Data.Message", b =>
                {
                    b.HasOne("API.Entities.AppUser", "recipient")
                        .WithMany("massageRecieved")
                        .HasForeignKey("recipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "sender")
                        .WithMany("massegeSent")
                        .HasForeignKey("senderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("recipient");

                    b.Navigation("sender");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.HasOne("API.Entities.AppUser", "appUser")
                        .WithMany("photo")
                        .HasForeignKey("appUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appUser");
                });

            modelBuilder.Entity("API.Entities.UserLike", b =>
                {
                    b.HasOne("API.Entities.AppUser", "likedUser")
                        .WithMany("likedByUsers")
                        .HasForeignKey("likedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "sourceUser")
                        .WithMany("likedUsers")
                        .HasForeignKey("sourceUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("likedUser");

                    b.Navigation("sourceUser");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("likedByUsers");

                    b.Navigation("likedUsers");

                    b.Navigation("massageRecieved");

                    b.Navigation("massegeSent");

                    b.Navigation("photo");
                });
#pragma warning restore 612, 618
        }
    }
}
