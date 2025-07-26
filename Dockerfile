FROM codercom/code-server:latest

# 可选：设置默认工作目录

COPY . /home/coder/project/
COPY ./settings.json /home/coder/.local/share/code-server/User/


WORKDIR /home/coder/project

USER root
RUN rm -rf /home/coder/project/settings.json
USER coder

RUN mkdir -p /home/coder/.config/code-server && \
    echo "bind-addr: 0.0.0.0:80\nauth: none\ncert: false" > /home/coder/.config/code-server/config.yaml

RUN mkdir -p /home/coder/.config/code-server/User && \
    echo '{ "security.workspace.trust.enabled": true, \n "workbench.activityBar.location": "hidden"}' > /home/coder/.config/code-server/User/settings.json

EXPOSE 80

CMD ["code-server", "/home/coder/project"]
