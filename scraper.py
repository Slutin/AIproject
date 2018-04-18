import json
import time
from urllib import request
from urllib.error import HTTPError, URLError
from bs4 import BeautifulSoup

HEADERS = {
    'User-Agent': 'Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.7) Gecko/2009021910 Firefox/3.0.7'
}

def find_rec_url(page_str, page_num):
    base_url = 'http://www.foodnetwork.com'
    search_url_str = 'search/a-z'
    url = '{}/{}/{}/p/{}'.format(base_url, search_url_str, page_str, page_num)

    try:
        soup = BeautifulSoup(request.urlopen(
            request.Request(url, headers=HEADERS)).read(), "html.parser")
        recipe_link_items = soup.select('h3.m-MediaBlock__a-Headline a')
        #print(recipe_link_items)
        recipe_links = [r['href'] for r in recipe_link_items]
        print('Read {} recipe links from {}'.format(len(recipe_links), url))
        print(recipe_links)
        return recipe_links
    except (HTTPError, URLError):
        print('Could not parse page {}'.format(url))
        return []


def scan_url_list(pages):
    f=open("recnum_scrap.csv", "w+")
    recipe_links = []
    f.write("title, ing, ing_num\n")
    for i in range(pages):
        temp = find_rec_url(1, i)
        recipe_links.extend(temp)
    list(set(recipe_links))
    for r in recipe_links:
        soup = BeautifulSoup(request.urlopen(
            request.Request("http:" + r, headers=HEADERS)).read(), "html.parser")
        title = soup.find("title").text
        ing_list = soup.select("li.o-Ingredients__a-ListItem input")
        ing_num = len(ing_list)
        if len(ing_list) != 0:
            full_ing = ""

            for ing in ing_list:
                mod_ing = ing.text.replace(",", "")
                mod_ing = mod_ing.replace("\u00a0", "")
                mod_ing = mod_ing.strip("\n")
                full_ing += mod_ing
                full_ing += " "
            f.write("%s, %s, %s\n" % (title, full_ing, ing_num))

            print("%s Complete" % title)

scan_url_list(800)
