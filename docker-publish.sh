docker login
docker-compose build
docker tag vdragomi/key-vault:latest vdragomi/key-vault:latest
docker push vdragomi/key-vault:latest
