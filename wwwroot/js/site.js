// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let likeCount = 0;
    let shareCount = 0;
    let commentCount = 0;

    function likeQuote(quoteId) {
        fetch(`/like/${quoteId}`, { method: 'POST' })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                // Update the UI based on the response
            });
    }

    function unlikeQuote(quoteId) {
        fetch(`/unlike/${quoteId}`, { method: 'POST' })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                // Update the UI based on the response
            });
    }


    function shareQuote() {
        shareCount++;
        document.getElementById("share-count").innerText = "Shares: " + shareCount;
        alert("Quote shared successfully!");
    }

    function commentQuote() {
        commentCount++;
        document.getElementById("comment-count").innerText = "Comments: " + commentCount;
        let comment = prompt("Enter your comment:");
        if (comment) {
            alert("Comment added: " + comment);
        }
    }

    function toggleTheme() {
        if (document.body.classList.contains('dark-theme')) {
            document.body.classList.remove('dark-theme');
            document.body.classList.add('light-theme');
            localStorage.setItem('theme', 'light-theme');
        } else {
            document.body.classList.remove('light-theme');
            document.body.classList.add('dark-theme');
            localStorage.setItem('theme', 'dark-theme');
        }
    }
    
    document.addEventListener('DOMContentLoaded', (event) => {
        const theme = localStorage.getItem('theme') || 'light-theme';
        document.body.classList.add(theme);
    });
    
    
    