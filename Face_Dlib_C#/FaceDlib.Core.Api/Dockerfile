FROM trojanolx/ubuntu16.04-dlib-opencv:v1.1
COPY . /app
WORKDIR /app
EXPOSE 15666/tcp
ENV LD_LIBRARY_PATH "/opencvsharp/src/build/OpenCvSharpExtern"
ENTRYPOINT ["dotnet", "FaceDlib.Core.Api.dll"]







##基于此镜像（运行环境）
#FROM microsoft/dotnet:2.1-runtime-stretch-slim AS base
#
##将当前目录下的文件复制到镜像里面去
#COPY . /app
#
##设置工作目录
#WORKDIR /app
#
## install System.Drawing native dependencies 图片的一些类库
#RUN apt-get update \
    #&& apt-get install -y --allow-unauthenticated \
        #libc6-dev \
        #libgdiplus \
        #libx11-dev \
     #&& rm -rf /var/lib/apt/lists/*
#
#
#
#RUN ln -s /lib/x86_64-linux-gnu/libdl.so.2 /lib/x86_64-linux-gnu/libdl.so
##开放端口
#EXPOSE 15666/tcp
##启动时执行的命令
#ENTRYPOINT ["dotnet", "FaceDlib.Core.Api.dll"]
#