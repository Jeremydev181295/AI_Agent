import os
import subprocess
from github import Github

# GitHub authentication
GITHUB_TOKEN = 'your_github_token'
REPO_NAME = 'your_forked_repo_name'
ORIGINAL_REPO = 'original_repo_owner/original_repo_name'
BRANCH_NAME = 'your_feature_branch'
FILE_NAME = 'path_to_your_file'
COMMIT_MESSAGE = 'your_commit_message'
PR_TITLE = 'your_pr_title'
PR_BODY = 'your_pr_description'

# Clone the repository
subprocess.run(['git', 'clone', f'https://github.com/{GITHUB_TOKEN}@github.com/{REPO_NAME}.git'])

# Navigate into the repository
os.chdir(REPO_NAME)

# Create a new branch
subprocess.run(['git', 'checkout', '-b', BRANCH_NAME])

# Make changes to your file
with open(FILE_NAME, 'w') as file:
    file.write("Your changes here")

# Add the changes to git
subprocess.run(['git', 'add', FILE_NAME])

# Commit the changes
subprocess.run(['git', 'commit', '-m', COMMIT_MESSAGE])

# Push the changes to your forked repository
subprocess.run(['git', 'push', '-u', 'origin', BRANCH_NAME])

# Authenticate with GitHub
g = Github(GITHUB_TOKEN)

# Get the repository
repo = g.get_repo(REPO_NAME)

# Create a pull request
repo.create_pull(
    title=PR_TITLE,
    body=PR_BODY,
    head=f"{GITHUB_TOKEN}:{BRANCH_NAME}",
    base="main"
)

print("Pull request created successfully!")
